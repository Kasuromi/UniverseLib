﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniverseLib.Runtime;

// Intentionally project-wide namespace so that its always easily accessible.
namespace UniverseLib
{
    public abstract class RuntimeProvider
    {
        public static RuntimeProvider Instance;

        public TextureUtilProvider TextureUtil;

        public RuntimeProvider()
        {
            Initialize();
        }

        public static void Init() =>
#if CPP
            Instance = new Runtime.Il2Cpp.Il2CppProvider();
#else
            Instance = new Runtime.Mono.MonoProvider();
#endif

        public abstract void Initialize();

        public abstract void StartCoroutine(IEnumerator routine);

        public abstract void Update();

        // Unity API handlers

        public abstract T AddComponent<T>(GameObject obj, Type type) where T : Component;

        public abstract ScriptableObject CreateScriptable(Type type);

        public abstract string LayerToName(int layer);

        public abstract UnityEngine.Object[] FindObjectsOfTypeAll(Type type);

        public abstract void GraphicRaycast(GraphicRaycaster raycaster, PointerEventData data, List<RaycastResult> list);

        public abstract GameObject[] GetRootGameObjects(Scene scene);

        public abstract int GetRootCount(Scene scene);

        public void SetColorBlockAuto(Selectable selectable, Color baseColor) 
            => SetColorBlock(selectable, baseColor, baseColor * 1.2f, baseColor * 0.8f);

        public abstract void SetColorBlock(Selectable selectable, ColorBlock colors);

        public abstract void SetColorBlock(Selectable selectable, Color? normal = null, Color? highlighted = null, Color? pressed = null,
            Color? disabled = null);

        internal virtual void ProcessOnPostRender()
        {
        }

        internal virtual void ProcessFixedUpdate()
        {
        }
    }
}