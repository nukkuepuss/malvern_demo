using UnityEngine;
using UnityEditor;
using System.Collections;

public class OBJAssetImporter : AssetPostprocessor {

    void OnPreprocessModel() {

        ModelImporter _modelImporter = assetImporter as ModelImporter;

        if (assetPath.Contains(".obj")) {

            _modelImporter.addCollider = true;
            _modelImporter.animationType = ModelImporterAnimationType.None;
            _modelImporter.importAnimation = false;
        }
    }
}
        
