namespace MoenenGames.VoxelRobot {

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class Demo : MonoBehaviour {



		// SUB
		public enum Movement {
			None = 0,
			PingPong = 1,
			Repeat = 2,
			Shoot = 3,
			RandomSpawn = 4,
		}



		[System.Serializable]
		public class ItemData {


			public Transform this[int index] {
				get {
					return ItemRoot[Mathf.Clamp(index, 0, Count - 1)];
				}
			}

			public int Count {
				get {
					return ItemRoot.Length;
				}
			}

			[SerializeField]
			private string Editor_Label = "";
			[SerializeField]
			private Transform[] ItemRoot;
			public bool UsingBlackRoom = true;
			public Movement Movement;
			public float MovementDistance = 4f;
			public float MovementDuration = 1.4f;
			public float LerpRate = 2f;
			public float Random = 0f;
			public float LifeTime = 1f;


			private void EditorWarningKiller () {
				string a = Editor_Label;
				Editor_Label = a;
			}


		}




		// VAR
		private Transform ShowingItemRoot {
			get {
				if (!m_ShowingItemRoot) {
					m_ShowingItemRoot = new GameObject("Showing Item Root").transform;
					m_ShowingItemRoot.SetParent(null);
					m_ShowingItemRoot.position = Vector3.zero;
					m_ShowingItemRoot.rotation = Quaternion.identity;
					m_ShowingItemRoot.localScale = Vector3.one;
				}
				return m_ShowingItemRoot;
			}
		}


		[SerializeField]
		private Text NameLabel;
		[SerializeField]
		private Transform SwitchPanelRoot;
		[SerializeField]
		private Toggle SwitchTGTemplate;
		[SerializeField]
		private Image Bar;
		[SerializeField]
		private Transform RoomRoot;
		[SerializeField]
		private Color BlackRoomColor;
		[SerializeField]
		private Color WhiteRoomColor;
		[SerializeField]
		private List<ItemData> Data;



		private int CurrentItemIndex = 0;
		private int CurrentSubIndex = 0;
		private bool UsingBlackTheme = true;
		private Transform m_ShowingItemRoot = null;
		private Coroutine ItemMovementCor = null;
		private Material[] RoomMats = null;




		// MSG
		private void Awake () {
			SwitchItem();
			MeshRenderer[] mrs = RoomRoot.GetComponentsInChildren<MeshRenderer>(true);
			RoomMats = new Material[mrs.Length];
			for (int i = 0; i < mrs.Length; i++) {
				RoomMats[i] = mrs[i].material;
			}
		}



		private void Update () {

			if (
				Input.GetKeyDown(KeyCode.Backspace)
			) {
				SpawnPrevItem();
			}


			if (
				Input.GetKeyDown(KeyCode.Space) ||
				Input.GetKeyDown(KeyCode.Return)
			) {
				SpawnNextItem();
			}


			if (Input.GetKeyDown(KeyCode.Tab)) {
				ChangeSubIndex((CurrentSubIndex + 1) % Data[CurrentItemIndex].Count);
				RespawnToggles();
				ResetName();
			}


			// Room Theme
			for (int i = 0; i < RoomMats.Length; i++) {
				RoomMats[i].color = Color.Lerp(
					RoomMats[i].color,
					UsingBlackTheme ? BlackRoomColor : WhiteRoomColor,
					Time.deltaTime * 5f
				);
			}

		}



		private void SwitchItem () {

			// Check
			if (Data.Count == 0 || Data[CurrentItemIndex][CurrentSubIndex] == null) {
				NameLabel.text = "";
				return;
			}

			// Name
			ResetName();

			// Switch Panel
			RespawnToggles();

			// Play
			RespawnItem(CurrentItemIndex, CurrentSubIndex);

		}




		public void RespawnItem (int index, int subIndex) {

			// Delete Old
			if (m_ShowingItemRoot) {
				DestroyImmediate(m_ShowingItemRoot.gameObject, false);
			}

			// Stop Movement
			if (ItemMovementCor != null) {
				StopCoroutine(ItemMovementCor);
			}

			// Bar
			FreshBar();

			// Add New
			ItemData data = Data[index];
			Transform tf = Instantiate(data[subIndex]);
			tf.SetParent(ShowingItemRoot);
			tf.localPosition = Vector3.zero;
			tf.localRotation = Quaternion.identity;
			tf.localScale = Vector3.one;

			switch (data.Movement) {
				case Movement.PingPong:
					ItemMovementCor = StartCoroutine(PingPongMovement(tf, data));
					break;
				case Movement.Repeat:
					ItemMovementCor = StartCoroutine(RepeatMovement(tf, data));
					break;
				case Movement.Shoot:
					Destroy(tf.gameObject);
					ItemMovementCor = StartCoroutine(ShootMovement(data));
					break;
				case Movement.RandomSpawn:
					Destroy(tf.gameObject);
					ItemMovementCor = StartCoroutine(RandomSpawnMovement(data));
					break;
			}

			// Room
			UsingBlackTheme = data.UsingBlackRoom;

		}



		// API
		public void UGUI_NextItem () {
			SpawnNextItem();
		}



		public void UGUI_PrevItem () {
			SpawnPrevItem();
		}



		public void UGUI_FiveStar () {
			Application.OpenURL(@"http://u3d.as/MKK");
		}






		// Logic
		private IEnumerator PingPongMovement (Transform ItemRoot, ItemData data) {
			while (ItemRoot) {

				Vector3 aimPos = data.MovementDistance * (
					Mathf.PingPong(Time.time, data.MovementDuration) > data.MovementDuration * 0.5f ?
					Vector3.right :
					Vector3.left
				);

				ItemRoot.localPosition = Vector3.Lerp(
					ItemRoot.localPosition,
					aimPos,
					Time.deltaTime * data.LerpRate
				);

				yield return new WaitForEndOfFrame();
			}
		}



		private IEnumerator RepeatMovement (Transform ItemRoot, ItemData data) {

			float lastShootTime = Time.time;

			while (ItemRoot) {

				if (Time.time > lastShootTime + data.MovementDuration) {
					lastShootTime = Time.time;
					ItemRoot.localPosition = Vector3.left * data.MovementDistance;
				}

				ItemRoot.localPosition = Vector3.Lerp(
					ItemRoot.localPosition,
					Vector3.right * data.MovementDistance,
					Time.deltaTime * data.LerpRate
				);

				yield return new WaitForEndOfFrame();
			}
		}



		private IEnumerator ShootMovement (ItemData data) {
			float lastShootTime = Time.time - data.MovementDuration;
			float currentDirY = 0f;
			while (true) {
				if (Time.time > lastShootTime + data.MovementDuration) {
					lastShootTime = Time.time;

					// Shoot
					Transform tf = Instantiate(data[CurrentSubIndex]);
					tf.SetParent(ShowingItemRoot);
					tf.localPosition = Vector3.zero;
					tf.localRotation = Quaternion.identity;
					tf.localScale = Vector3.one;

					Destroy(tf.gameObject, data.LifeTime);

					Rigidbody rig = tf.GetComponent<Rigidbody>();
					if (rig) {
						rig.velocity = Quaternion.Euler(0f, currentDirY, 0f) * Vector3.forward * data.MovementDistance;
					}
					currentDirY += Time.deltaTime * (data.LerpRate + (2f * Random.value - 1f) * data.Random);
					currentDirY %= 360f;

				}
				yield return new WaitForEndOfFrame();
			}
		}



		private IEnumerator RandomSpawnMovement (ItemData data) {
			float lastShootTime = Time.time - data.MovementDuration;
			while (true) {
				if (Time.time > lastShootTime + data.MovementDuration) {
					lastShootTime = Time.time;
					Vector3 pos = Random.insideUnitSphere * data.MovementDistance;
					pos.y = 0f;

					// Spawn
					Transform tf = Instantiate(data[CurrentSubIndex]);
					tf.SetParent(ShowingItemRoot);
					tf.localPosition = pos;
					tf.localRotation = Quaternion.identity;
					tf.localScale = Vector3.one;

					Destroy(tf.gameObject, data.LifeTime);



				}
				yield return new WaitForEndOfFrame();
			}
		}



		private void SpawnNextItem () {
			CurrentItemIndex = (int)Mathf.Repeat(CurrentItemIndex + 1, Data.Count);
			SwitchItem();
		}


		private void SpawnPrevItem () {
			CurrentItemIndex = (int)Mathf.Repeat(CurrentItemIndex - 1, Data.Count);
			SwitchItem();
		}


		private void ChangeSubIndex (int index) {
			CurrentSubIndex = index;
			RespawnItem(CurrentItemIndex, CurrentSubIndex);
		}


		private void RespawnToggles () {
			// Clear
			Toggle[] tgs = SwitchPanelRoot.GetComponentsInChildren<Toggle>(true);
			for (int i = 0; i < tgs.Length; i++) {
				tgs[i].onValueChanged.RemoveAllListeners();// I did this in demo code....
				DestroyImmediate(tgs[i].gameObject, false);
			}
			// Check
			if (Data[CurrentItemIndex].Count <= 1) {
				return;
			}
			// Add
			for (int i = 0; i < Data[CurrentItemIndex].Count; i++) {
				Toggle tg = Instantiate(SwitchTGTemplate);
				if (!tg.gameObject.activeSelf) {
					tg.gameObject.SetActive(true);
				}
				tg.transform.SetParent(SwitchPanelRoot);
				tg.transform.localScale = Vector3.one;
				tg.transform.localRotation = Quaternion.identity;
				int index = i;
				int count = Data[CurrentItemIndex].Count;
				tg.isOn = i == Mathf.Clamp(CurrentSubIndex, 0, count - 1);
				tg.onValueChanged.AddListener((isOn) => {
					if (isOn) {
						ChangeSubIndex(index % count);
						ResetName();
					}
				});
				Text text = tg.GetComponentInChildren<Text>(true);
				if (text) {
					string _subName = Data[CurrentItemIndex][i].name;
					if (!string.IsNullOrEmpty(_subName)) {
						text.text = _subName[_subName.Length - 1].ToString();
					}
				}
			}
		}


		private void ResetName () {
			string _name = Data[CurrentItemIndex][CurrentSubIndex].name;
			NameLabel.text = _name + "\n<size=18>" + (CurrentItemIndex + 1).ToString("00") + " / " + Data.Count.ToString("00") + "</size>";
		}


		private void FreshBar () {
			Bar.fillAmount = ((CurrentItemIndex + 0.1f) / (Data.Count - 1f));
		}

	}
}