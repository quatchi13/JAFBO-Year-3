using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryParent : MonoBehaviour
{
    public abstract string Name { get; }

    public abstract GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation);
}

public class Boundaries : FactoryParent
{
    public override string Name { get { return "boundaries"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class Ground : FactoryParent
{
    public override string Name { get { return "ground"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class Water : FactoryParent
{
    public override string Name { get { return "water"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class Rock : FactoryParent
{
    public override string Name { get { return "rock"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class Tree : FactoryParent
{
    public override string Name { get { return "tree"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}


