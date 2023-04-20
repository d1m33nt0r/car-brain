using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class GridDrawer
    {
        private Vector2 offset;
        private Rect rect;
        private Vector2 drag;
        
        public GridDrawer(Rect rect)
        {
            this.rect = rect;
        }
        
        public void Draw()
        {
            DrawGrid(20, 0.2f, new Color(0.1f, 0.1f, 0.1f, 0.05f));
            DrawGrid(100, 0.4f, new Color(0.1f, 0.1f, 0.1f, 0.1f));
        }
        
        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            var widthDivs = Mathf.CeilToInt(rect.width / gridSpacing);
            var heightDivs = Mathf.CeilToInt(rect.height / gridSpacing);
 
            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
 
            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);
 
            for (var i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, 
                    new Vector3(gridSpacing * i, rect.height, 0f) + newOffset);
            }
 
            for (var j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, 
                    new Vector3(rect.width, gridSpacing * j, 0f) + newOffset);
            }
 
            Handles.color = Color.white;
            Handles.EndGUI();
        }
    }
}