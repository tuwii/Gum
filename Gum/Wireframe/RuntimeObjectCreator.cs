﻿using Microsoft.Xna.Framework.Graphics;
using RenderingLibrary;
using RenderingLibrary.Graphics;
using RenderingLibrary.Math.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gum.Wireframe
{
    public static class RuntimeObjectCreator
    {

        public static IRenderable TryHandleAsBaseType(string baseType, ISystemManagers managers)
        {
            var systemManagers = managers as SystemManagers;
            IRenderable containedObject = null;
            switch (baseType)
            {
#if MONOGAME || FNA

                case "Container":
                case "Component": // this should never be set in Gum, but there could be XML errors or someone could have used an old Gum...
                    if (GraphicalUiElement.ShowLineRectangles)
                    {
                        LineRectangle lineRectangle = new LineRectangle(systemManagers);
                        lineRectangle.Color = System.Drawing.Color.FromArgb(
#if GUM
                            255,
                            Gum.ToolStates.GumState.Self.ProjectState.GeneralSettings.OutlineColorR,
                            Gum.ToolStates.GumState.Self.ProjectState.GeneralSettings.OutlineColorG,
                            Gum.ToolStates.GumState.Self.ProjectState.GeneralSettings.OutlineColorB
#else
                        255,255,255,255
#endif
                            );

                        containedObject = lineRectangle;
                    }
                    else
                    {
                        containedObject = new InvisibleRenderable();
                    }
                    break;

                case "Rectangle":
                    LineRectangle rectangle = new LineRectangle(systemManagers);
                    rectangle.IsDotted = false;
                    containedObject = rectangle;
                    break;
                case "Circle":
                    LineCircle circle = new LineCircle(systemManagers);
                    circle.CircleOrigin = CircleOrigin.TopLeft;
                    containedObject = circle;
                    break;
                case "Polygon":
                    LinePolygon polygon = new LinePolygon(systemManagers);
                    containedObject = polygon;
                    break;
                case "ColoredRectangle":
                    SolidRectangle solidRectangle = new SolidRectangle();
                    containedObject = solidRectangle;
                    break;
                case "Sprite":
                    Texture2D texture = null;

                    Sprite sprite = new Sprite(texture);
                    containedObject = sprite;

                    break;
                case "NineSlice":
                    {
                        NineSlice nineSlice = new NineSlice();
                        containedObject = nineSlice;
                    }
                    break;
                case "Text":
                    {
                        Text text = new Text(systemManagers, "");
                        containedObject = text;
                    }
                    break;
#endif

#if SKIA
                case  "Arc":
                    return new SkiaGum.Renderables.RenderableArc();
                case  "ColoredCircle":
                    return new SkiaGum.Renderables.RenderableCircle();
                case  "RoundedRectangle":
                    return new SkiaGum.Renderables.RenderableRoundedRectangle();

#endif


            }
            return containedObject;
        }

    }
}
