﻿using System;
using System.Collections.Generic;

namespace ArknightsMap
{

    public interface IRouteDrawer : IDrawer
    {
        public Route Route { get; }
        public bool ShowRouteLength { get; set; }
        public void DrawRoute();
        public void DrawCheckPoint(int checkPointIndex);
        public void DrawMoveLine(int checkPointIndex);
    }
}