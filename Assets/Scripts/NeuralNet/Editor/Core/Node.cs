using System;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Neurons;
using NeuralNet.Core.Neurons.Output;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class Node : StylizedDrawer<EmptyDrawerArgs>
    {
        public Neuron neuron;
        
        public string title;
        public bool isDragged;
        public bool isSelected;
        
        public InNodeNodeConnectionPoint InNodeNodePoint;
        public OutNodeNodeConnectionPoint OutNodeNodePoint;
        
        public GUIStyle defaultNodeStyle;
        public GUIStyle selectedNodeStyle;
        
        public Action<Node> OnRemoveNode;

        public Node(Neuron neuron,Vector2 position, 
            Action<NodeConnectionPoint> OnClickInPoint, 
            Action<NodeConnectionPoint> OnClickOutPoint, 
            Action<Node> OnClickRemoveNode)
        {
            this.neuron = neuron;
            rect = new Rect(position.x, position.y, 120, 82);
            InNodeNodePoint = new InNodeNodeConnectionPoint(this, ConnectionPointType.In, OnClickInPoint);
            OutNodeNodePoint = new OutNodeNodeConnectionPoint(this, ConnectionPointType.Out, OnClickOutPoint);
            OnRemoveNode = OnClickRemoveNode;
        }
 
        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        protected override void ApplyStyles()
        {
            defaultNodeStyle = new GUIStyle();
            defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);
            
            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

            style = defaultNodeStyle;
        }

        public override void Draw(EmptyDrawerArgs args)
        {
            if (neuron.neuronType != NeuronType.Input)
                InNodeNodePoint.Draw(args);
            if (neuron.neuronType != NeuronType.Output)
                OutNodeNodePoint.Draw(args);
            
            var position = rect.position + new Vector2(0, -20);
            GUILayout.BeginArea(new Rect(position, new Vector2(120, 20)));
            GUILayout.Label($"Neuron ID: {neuron.id}", new GUIStyle{alignment = TextAnchor.MiddleCenter});
            GUILayout.EndArea();
            EditorGUI.DrawRect(rect, new Color(0.14f, 0.15f, 0.2196079f, 0.75f));
            
            GUILayout.BeginArea(rect, new GUIStyle{border = new RectOffset()});
            
            GUILayout.BeginHorizontal();
            neuron.data = EditorGUILayout.DoubleField(neuron.data);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            var prevActivationValue = neuron.activationType;
            var nextActivationValue = (ActivationType)EditorGUILayout.EnumPopup(neuron.activationType);
            if (prevActivationValue != nextActivationValue) neuron.SetActivationType(nextActivationValue);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            var prevNeuronTypeValue = neuron.neuronType;
            var nextNeuronTypeValue = (NeuronType)EditorGUILayout.EnumPopup(neuron.neuronType);
            if (nextNeuronTypeValue != prevNeuronTypeValue) neuron.ChangeNeuronType(nextNeuronTypeValue);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Bias   ");
            neuron.bias = EditorGUILayout.FloatField(neuron.bias);
            GUILayout.EndHorizontal();
            
            GUILayout.EndArea();
        }
 
        public bool ProcessEvents(Event e, Vector2 mousePosition, float zoom)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(mousePosition  / zoom))
                        {
                            isDragged = true;
                            GUI.changed = true;
                            isSelected = true;
                            style = selectedNodeStyle;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                            style = defaultNodeStyle;
                        }
                    }
                    if (e.button == 1 && isSelected && rect.Contains(mousePosition  / zoom))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }
                    break;
 
                case EventType.MouseUp:
                    isDragged = false;
                    break;
 
                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        var newDelta = e.delta / zoom;
                        Drag(newDelta);
                        e.Use();
                        neuron.position = rect.position;
                        return true;
                    }
                    break;
            }
            return false;
        }
        
        private void ProcessContextMenu()
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }
 
        private void OnClickRemoveNode()
        {
            if (OnRemoveNode != null)
            {
                OnRemoveNode(this);
            }
        }
    }
}