using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    /// <summary>
    /// 바람
    /// </summary>
    public class Wind : ILogicRenderable
    {

        /// <summary>
        /// 바람의 방향-세기가 바뀐 마지막 시간
        /// </summary>
        private static DateTime preChangeWindTime = DateTime.MinValue;

        /// <summary>
        /// 현재 바람 방향-세기 배열의 인덱스
        /// </summary>
        private int currentWindForceIndex = 0;

        /// <summary>
        /// 바람의 방향-세기가 바뀌는 주기  nanosecond
        /// </summary>
        private const int changeWindInterval = 50000000;

        /// <summary>
        /// 바람의 방향-세기 주기 마다 바뀝니다.
        /// </summary>
        private static int[] windForceArray = new int[] { 0, 2, 0, -1, 0, 1, 0, -2};

        /// <summary>
        /// 바람의세기. 값이 플러스이면 오른쪽 마이너스 이면 왼쪽
        /// </summary>
        private int force = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>다음 WindForceIndex를 return 해줍니다.</returns>
        private int GetNextWindForceIndex()
        {
            currentWindForceIndex++;
            currentWindForceIndex = currentWindForceIndex % windForceArray.Length;
            return currentWindForceIndex;
        }

        #region 싱글톤 코드
        private Wind()
        {
            Force = windForceArray[currentWindForceIndex];
        }

        private static Wind instance = null;
        
        public static Wind Instance()
        {
            if (instance == null)
                instance = new Wind();

            return instance;
        }
        #endregion

        /// <summary>
        /// ILogicRenderable 구현 함수
        /// </summary>
        public void LogicRender()
        {
            if (DateTime.Now.Ticks - preChangeWindTime.Ticks > changeWindInterval)
            {
                Force = windForceArray[GetNextWindForceIndex()];
            }
        }

        public int Force
        {
            get
            {
                return force;
            }

            set
            {
                force = value;
                preChangeWindTime = DateTime.Now;
            }
        }
    }
}
