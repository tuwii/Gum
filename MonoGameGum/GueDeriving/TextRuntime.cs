﻿using Gum.Wireframe;
using RenderingLibrary;
using RenderingLibrary.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameGum.GueDeriving
{
    public class TextRuntime : GraphicalUiElement
    {
        Text mContainedText;
        Text ContainedText
        {
            get
            {
                if (mContainedText == null)
                {
                    mContainedText = this.RenderableComponent as Text;
                }
                return mContainedText;
            }
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get => ContainedText.HorizontalAlignment;
            set => ContainedText.HorizontalAlignment = value;
        }

        public VerticalAlignment VerticalAlignment
        {
            get => ContainedText.VerticalAlignment;
            set => ContainedText.VerticalAlignment = value;
        }

        public string Text
        {
            get
            {
                return ContainedText.RawText;
            }
            set
            {
                var widthBefore = ContainedText.WrappedTextWidth;
                var heightBefore = ContainedText.WrappedTextHeight;
                if (this.WidthUnits == Gum.DataTypes.DimensionUnitType.RelativeToChildren)
                {
                    // make it have no line wrap width before assignign the text:
                    ContainedText.Width = 0;
                }
                ContainedText.RawText = value;
                NotifyPropertyChanged();
                var shouldUpdate = widthBefore != ContainedText.WrappedTextWidth || heightBefore != ContainedText.WrappedTextHeight;
                if (shouldUpdate)
                {
                    UpdateLayout(Gum.Wireframe.GraphicalUiElement.ParentUpdateType.IfParentWidthHeightDependOnChildren | Gum.Wireframe.GraphicalUiElement.ParentUpdateType.IfParentStacks, int.MaxValue / 2);
                }
            }
        }

        public TextRuntime(bool fullInstantiation = true)
        {
            if(fullInstantiation)
            {
                var textRenderable = new Text(SystemManagers.Default);
                textRenderable.RenderBoundary = false;
                
                SetContainedObject(textRenderable);
                Width = 0;
                WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren;
                Height = 0;
                HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren;

                textRenderable.RawText = "Hello World";
            }
        }

        public void AddToManagers() => base.AddToManagers(SystemManagers.Default, layer:null);
    }
}
