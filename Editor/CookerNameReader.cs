// AlwaysTooLate.Core (c) 2018-2019 Always Too Late. All rights reserved.

using System;
using UnityEditor;
using UnityEngine;

namespace AlwaysTooLate.GameCooker
{
    /// <summary>
    ///     Name reader helper class for Cooker
    /// </summary>
    public class CookerNameReader
    {
        private readonly Action _onCancel;
        private readonly Action<string> _onDone;
        private string _name;

        private bool _nullName;

        // private
        private CookerNameReader()
        {
        }

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="onDone">OnDone action, invoked after pressing 'ok' button.</param>
        /// <param name="onCancel">OnCancel action, invoked after pressing 'cancel' button.</param>
        public CookerNameReader(string title, Action<string> onDone, Action onCancel)
        {
            Title = title;
            _onDone = onDone;
            _onCancel = onCancel;
            _name = "";
        }

        /// <summary>
        ///     The title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     Draw on the screen.
        /// </summary>
        public void Draw()
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2.0f - 200.0f, Screen.height / 2.0f - 120.0f, 400.0f, 240.0f));
            {
                GUILayout.BeginVertical();
                {
                    GUILayout.Label(Title);
                    _name = EditorGUILayout.TextField("Name", _name);

                    if (_nullName)
                        GUILayout.Label("Please enter a name.");

                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Ok"))
                        {
                            if (string.IsNullOrEmpty(_name))
                            {
                                _nullName = true;
                                return;
                            }

                            _onDone(_name);
                            _nullName = false;
                        }

                        if (GUILayout.Button("Cancel")) _onCancel?.Invoke();
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }
    }
}