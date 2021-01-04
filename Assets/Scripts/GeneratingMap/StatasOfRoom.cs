using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace itc 

{ 
public class StatasOfRoom : MonoBehaviour {

	
		public   int[] thisRoomStats= new int[5];

		//0-up 1-right 2-down 3-left 4-nubmer of exits
		public	void CloseDoor(int ExitNumber)
		{
			thisRoomStats [ExitNumber] = 0;
		}

	}
}
