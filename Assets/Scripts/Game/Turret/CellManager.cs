using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellManager : MonoBehaviour
{
    TurretBuilderManager builder;
    Renderer cellRenderer;
    GameObject turretObject;
    Color defaultColor;

    void Start()
    {
        cellRenderer = GetComponent<Renderer>();
        defaultColor = cellRenderer.material.GetColor("_Color");
        builder = TurretBuilderManager.instance;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (builder.GetSelectedTurretToBuild() == null || turretObject != null)
        {
            return;
        }

        cellRenderer.material.SetColor("_Color", Color.yellow);
    }

    private void OnMouseExit()
    {
        cellRenderer.material.SetColor("_Color", defaultColor);
    }

    private void OnMouseDown()
    {
        if (builder.GetSelectedTurretToBuild() == null || turretObject != null)
        {
            return;
        }

        GameObject buildTurret = builder.GetSelectedTurretToBuild();
        Vector3 scaleOffset = new Vector3(0, transform.localScale.y, 0);
        turretObject = Instantiate(buildTurret, transform.position + scaleOffset, transform.rotation);
        builder.SetDefault();
    }
}
