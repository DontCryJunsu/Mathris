using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Service;
public class StageManager : Singleton<StageManager>
{
    // 1스테이지 -1 을 넣어주고 새로 시작하는 인덱스는 1스테이지 인덱스넣어주기
    public int currentStage = 1;
    // 숫자 배열에 대한 인덱스
    public int numberArrayIndex = 0;
    public int[] numberArray;

    // 연산 숫자 가 들어갈 수 있는 인덱스
    public int operatorNumberIndex = 0;
    public List<int> numbers;
    public Transform[] numberTransforms;
}
