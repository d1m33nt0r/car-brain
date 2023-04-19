using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class GridDrawer
    {
        private Vector2 offset;
        private NeuralEditorWindow neuralEditorWindow;
        private Vector2 drag;
        
        public GridDrawer(NeuralEditorWindow neuralEditorWindow)
        {
            this.neuralEditorWindow = neuralEditorWindow;
        }
        
        public void Draw()
        {
            DrawGrid(20, 0.2f, new Color(0.1f, 0.1f, 0.1f, 0.05f));
            DrawGrid(100, 0.4f, new Color(0.1f, 0.1f, 0.1f, 0.1f));
        }
        
        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            var widthDivs = Mathf.CeilToInt(neuralEditorWindow.position.width / gridSpacing);
            var heightDivs = Mathf.CeilToInt(neuralEditorWindow.position.height / gridSpacing);
 
            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
 
            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);
 
            for (var i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, 
                    new Vector3(gridSpacing * i, neuralEditorWindow.position.height, 0f) + newOffset);
            }
 
            for (var j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, 
                    new Vector3(neuralEditorWindow.position.width, gridSpacing * j, 0f) + newOffset);
            }
 
            Handles.color = Color.white;
            Handles.EndGUI();
        }
    }
}