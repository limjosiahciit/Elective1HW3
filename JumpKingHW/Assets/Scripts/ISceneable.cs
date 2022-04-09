using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneable <S>
{
    void LoadScene(S sceneIndex);
}
