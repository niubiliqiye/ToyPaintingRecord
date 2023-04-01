using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tooltip", menuName = "Tooltip/Tooltip Data")]
public class ToolTipData_SO : ScriptableObject
{
    [TextArea]public string[] tooltip;
}
