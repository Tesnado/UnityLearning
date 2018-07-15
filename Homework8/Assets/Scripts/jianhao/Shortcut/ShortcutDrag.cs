using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutDrag : UIDragDropItem
{
    private Shortcut shortcut;
    private Shortcut SC
    {
        get
        {
            if (shortcut == null)
                shortcut = GetComponent<Shortcut>();
            return shortcut;
        }
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            if (surface.name == "Shortcut(Clone)")
            {
                //移到其他Shortcut上
                Transform tempTran = transform.parent;
                transform.parent = surface.transform.parent;
                surface.transform.parent = tempTran;
                surface.transform.localPosition = Vector3.zero;
                Shortcut sc = surface.GetComponent<Shortcut>();
                int temp = SC.containerID;
                SC.containerID = sc.containerID;
                sc.containerID = temp;
            }
            else if (surface.name.Contains("ShortcutContainer"))
            {
                transform.parent = surface.transform;
                ShortcutManager.instance.Disuse(SC.containerID);
                SC.containerID = surface.name[0] - '0';
                ShortcutManager.instance.Use(SC.containerID);
            }
            else if (surface.name == "ShortcutTrashCan")
            {
                Destroy(gameObject);
            }
        }
        transform.localPosition = Vector3.zero;
    }
}