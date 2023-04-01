using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    /// <summary>
    /// 画笔颜色
    /// </summary>
    private Color Pen_Colour = Color.black;
    /// <summary>
    /// 画笔宽度
    /// </summary>
    private int Pen_Width = 6;

    public LayerMask Drawing_Layers;

    /// <summary>
    /// 绘制的图片
    /// </summary>
    private Sprite drawable_sprite;
    
    /// <summary>
    /// 绘制图片的2D纹理
    /// </summary>
    private Texture2D drawable_texture;

    /// <summary>
    /// 以前绘制的点
    /// </summary>
    private Vector2 previous_drag_position;
    
    private Color[] clean_colours_array;
    private Collider2D[] rayResult = new Collider2D[2];
    
    /// <summary>
    /// 绘制后的颜色
    /// </summary>
    private Color32[] cur_colors;

    /// <summary>
    /// 当前拖动时不绘制
    /// </summary>
    private bool no_drawing_on_current_drag = false;
    
    /// <summary>
    /// 先前按住鼠标
    /// </summary>
    private bool mouse_was_previously_held_down = false;

    void Awake()
    {
        drawable_sprite = this.GetComponent<SpriteRenderer>().sprite;
        drawable_texture = drawable_sprite.texture;

        clean_colours_array = new Color[(int)drawable_sprite.rect.width * (int)drawable_sprite.rect.height];
        clean_colours_array = drawable_texture.GetPixels();
    }

    void Update()
    {
        if (GameManager.Instatic.isDraw)
        {
                    bool mouse_held_down = Input.GetMouseButton(0);
                    if (mouse_held_down && !no_drawing_on_current_drag)
                    {
                        //获取鼠标在世界坐标系下的位置
                        Vector2 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
                        Collider2D hit = Physics2D.OverlapPoint(mouse_world_position, Drawing_Layers.value);
                        if (hit != null && hit.transform != null)
                        {
                            PenBrush(mouse_world_position);
                            //current_brush(mouse_world_position);
                        }
                        else
                        {
                            previous_drag_position = Vector2.zero;
                            if (!mouse_was_previously_held_down)
                            {
                                no_drawing_on_current_drag = true;
                            }
                        }
                    }
                    else if (!mouse_held_down)
                    {
                        previous_drag_position = Vector2.zero;
                        no_drawing_on_current_drag = false;
                    }
            
                    mouse_was_previously_held_down = mouse_held_down;
        }
    }

    protected void OnDestroy()
    {
        ResetCanvas();
    }

    /// <summary>
    ///  重置画布
    /// </summary>
    public void ResetCanvas()
    {
        drawable_texture.SetPixels(clean_colours_array);
        drawable_texture.Apply();
    }

    /// <summary>
    /// 笔刷功能实现
    /// </summary>
    /// <param name="world_point">鼠标的世界坐标系位置</param>
    public void PenBrush(Vector2 world_point)
    {
        //将鼠标的位置转换成像素坐标
        Vector2 pixel_pos = WorldToPixelCoordinates(world_point);

        cur_colors = drawable_texture.GetPixels32();

        if (previous_drag_position == Vector2.zero)
        {
            MarkPixelsToColour(pixel_pos, Pen_Width, Pen_Colour);
        }
        else
        {
            ColourBetween(previous_drag_position, pixel_pos, Pen_Width, Pen_Colour);
        }

        ApplyMarkedPixelChanges();

        previous_drag_position = pixel_pos;
    }

    private Vector2 WorldToPixelCoordinates(Vector2 world_position)
    {
        Vector3 local_pos = transform.InverseTransformPoint(world_position);

        float pixelWidth = drawable_sprite.rect.width;
        float pixelHeight = drawable_sprite.rect.height;
        float unitsToPixels = pixelWidth / drawable_sprite.bounds.size.x * transform.localScale.x;

        float centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
        float centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

        Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

        return pixel_pos;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="start_point">开始位置</param>
    /// <param name="end_point">结束位置</param>
    /// <param name="width">画笔宽度</param>
    /// <param name="color">画笔颜色</param>
    private void ColourBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
    {
        //获取两个点之间的距离
        float distance = Vector2.Distance(start_point, end_point);
        //获取两点之间的方向
        Vector2 direction = (start_point - end_point).normalized;

        //当前的位置
        Vector2 cur_position = start_point;
        float lerp_steps = 1 / distance;

        for (float lerp = 0; lerp <= 1; lerp += lerp_steps)
        {
            cur_position = Vector2.Lerp(start_point, end_point, lerp);
            MarkPixelsToColour(cur_position, width, color);
        }
    }

    /// <summary>
    /// 改变当前画笔位置附近的像素颜色
    /// </summary>
    /// <param name="center_pixel">画笔在纹理中的位置</param>
    /// <param name="pen_thickness">画笔的宽度</param>
    /// <param name="color_of_pen">画笔的颜色</param>
    private void MarkPixelsToColour(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
    {
        int center_x = (int)center_pixel.x;
        int center_y = (int)center_pixel.y;

        for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
        {
            if (x >= (int)drawable_sprite.rect.width || x < 0)
                continue;

            for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
            {
                MarkPixelToChange(x, y, color_of_pen);
            }
        }
    }

    /// <summary>
    /// 改变档位置的像素颜色
    /// </summary>
    /// <param name="x">当前X轴位置</param>
    /// <param name="y">当前X轴位置</param>
    /// <param name="color">需要改变的颜色</param>Y
    private void MarkPixelToChange(int x, int y, Color color)
    {
        int array_pos = y * (int)drawable_sprite.rect.width + x;

        if (array_pos > cur_colors.Length-1 || array_pos < 0)
            return;

        cur_colors[array_pos] = color;
    }

    private void ApplyMarkedPixelChanges()
    {
        drawable_texture.SetPixels32(cur_colors);
        drawable_texture.Apply();
    }

    public void SetPenColor(int index)
    {
        Pen_Colour = index switch
        {
            1 => Color.red,
            2 => Color.blue,
            3 => Color.green,
            _ => Color.black,
        };
    }

    public void SetPenWidth()
    {
        Slider slider = FindObjectOfType<Slider>();
        Pen_Width = (int)(6 * slider.value);
    }
}