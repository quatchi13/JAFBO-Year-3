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

public class hRail : FactoryParent
{
    public override string Name { get { return "hRail"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class vRail : FactoryParent
{
    public override string Name { get { return "vRail"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class swRail : FactoryParent
{
    public override string Name { get { return "swRail"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class neRail : FactoryParent
{
    public override string Name { get { return "neRail"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class nwRail : FactoryParent
{
    public override string Name { get { return "nwRail"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class seRail : FactoryParent
{
    public override string Name { get { return "seRail"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class goldmine : FactoryParent
{
    public override string Name { get { return "goldmine"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class tree : FactoryParent
{
    public override string Name { get { return "tree"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class cactus : FactoryParent
{
    public override string Name { get { return "cactus"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class barrel : FactoryParent
{
    public override string Name { get { return "barrel"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class b1Cliff : FactoryParent
{
    public override string Name { get { return "b1Cliff"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class b2Cliff : FactoryParent
{
    public override string Name { get { return "b2Cliff"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class b6Cliff : FactoryParent
{
    public override string Name { get { return "b6Cliff"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class f1Cliff : FactoryParent
{
    public override string Name { get { return "f1Cliff"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class f2Cliff : FactoryParent
{
    public override string Name { get { return "f2Cliff"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class f3Cliff : FactoryParent
{
    public override string Name { get { return "f3Cliff"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class f4Cliff : FactoryParent
{
    public override string Name { get { return "f4Cliff"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class f5Cliff : FactoryParent
{
    public override string Name { get { return "f5Cliff"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}

public class f6Cliff : FactoryParent
{
    public override string Name { get { return "f6Cliff"; } }

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

public class Water : FactoryParent
{
    public override string Name { get { return "water"; } }

    public override GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject tile = Instantiate(prefab, position, rotation);
        return tile;
    }
}





