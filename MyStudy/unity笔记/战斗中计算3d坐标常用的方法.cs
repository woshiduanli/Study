 // 返回 用center用圆心， radius为半径的圆上的随机点 
    public Vector3 OnPostRender(Vector3 center, float radius)
    {
        Vector3 v = new Vector3(0, 0, 1);
        v = Quaternion.Euler(0, Random.Range(0, 360), 0) * v;
        Vector3 pos = v * radius;
        Vector3 newPos = center + pos;
        return newPos;
    }
