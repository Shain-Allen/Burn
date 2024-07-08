using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EventTrigger))]
public class EventTriggerEditor : Editor
{
    bool foldoutOpen = true; //Keeps track of whether or not foldout menu is open

    override public void OnInspectorGUI()
    {
        //This draws the default inspector as well if I had it uncommented (saving for future reference)
        //base.OnInspectorGUI();

        // Saving a reference to the script's inspector I want to edit
        var eventTrigger = target as EventTrigger;

        // Foldout menu formatting
        string status = "What to trigger?";
        GUIStyle myFoldoutStyle = new(EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;
        foldoutOpen = EditorGUILayout.Foldout(foldoutOpen, status, true, myFoldoutStyle);


        // If the foldout menu is open, draw all of the trigger toggles
        if (foldoutOpen)
        {
            EditorGUI.indentLevel++;

            // Using disable GameObject
            eventTrigger.useDisable = EditorGUILayout.Toggle("Disable a GameObject", eventTrigger.useDisable);

            if (eventTrigger.useDisable)
            {
                EditorGUI.indentLevel++;
                bool allowSceneObjects = !EditorUtility.IsPersistent(target);
                eventTrigger.objToDisable = (GameObject)EditorGUILayout.ObjectField("Object to Disable: ", eventTrigger.objToDisable, typeof(GameObject), allowSceneObjects);
                EditorGUI.indentLevel--;
            }

            // Using enable GameObject
            eventTrigger.useEnable = EditorGUILayout.Toggle("Enable a GameObject", eventTrigger.useEnable);

            if (eventTrigger.useEnable)
            {
                EditorGUI.indentLevel++;
                bool allowSceneObjects = !EditorUtility.IsPersistent(target);
                eventTrigger.objToEnable = (GameObject)EditorGUILayout.ObjectField("Object to Enable: ", eventTrigger.objToEnable, typeof(GameObject), allowSceneObjects);
                EditorGUI.indentLevel--;
            }

            // Using play audio
            eventTrigger.useAudio = EditorGUILayout.Toggle("Trigger Audio", eventTrigger.useAudio);

            if (eventTrigger.useAudio)
            {
                EditorGUI.indentLevel++;
                bool allowSceneObjects = !EditorUtility.IsPersistent(target);
                eventTrigger.soundToPlay = (AudioSource)EditorGUILayout.ObjectField("Sound to Play: ", eventTrigger.soundToPlay, typeof(AudioSource), allowSceneObjects);
                EditorGUI.indentLevel--;
            }

            // Using play animation
            eventTrigger.useAnimation = EditorGUILayout.Toggle("Trigger Animation", eventTrigger.useAnimation);

            if (eventTrigger.useAnimation)
            {
                EditorGUI.indentLevel++;
                bool allowSceneObjects = !EditorUtility.IsPersistent(target);
                eventTrigger.animationToPlay = (Animator)EditorGUILayout.ObjectField("Animation to Play: ", eventTrigger.animationToPlay, typeof(Animator), allowSceneObjects);
                eventTrigger.animationName = EditorGUILayout.TextField("Animation Name: ", eventTrigger.animationName);
                EditorGUI.indentLevel--;
            }
        }

        EditorGUILayout.Space();

        eventTrigger.requiredPhotos = EditorGUILayout.IntSlider("Required # of Photos: ", eventTrigger.requiredPhotos, 0, 10);
    }
}
