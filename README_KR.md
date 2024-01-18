# '딸각' 배치

디자이너와 협업하여 사용할 수 있는 오브젝트 배치 툴

# 사용 방법

1. 프리팹을 세팅하세요

![image](https://github.com/twozeronine/oban-exporter/assets/67315288/45f41399-1028-46fe-9dbc-a1b052670935)

드래그 앤 드롭으로 생성될 오브젝트를 설정해줍니다.

2. 생성될 좌표지점과 각 오브젝트 끼리의 간격인 Offset을 입력해주세요

![image](https://github.com/twozeronine/oban-exporter/assets/67315288/88b6c718-9538-4f0c-b016-841bc4ee7df9)

3. Object가 추가될 좌표를 입력해주세요

![image](https://github.com/twozeronine/oban-exporter/assets/67315288/51e95d23-272e-41e9-8090-076dd71ce47c)

Create 버튼 클릭 시 오브젝트가 좌표값으로 생성됩니다.

Clear 클릭 시 생성되었던 모든 오브젝트가 삭제되니 주의해주세요

4. 저장과 불러오기

![image](https://github.com/twozeronine/oban-exporter/assets/67315288/34c5acfe-6634-4ac2-90fc-0aa96601e8ff)

세팅한 Object의 좌표값을 JSON 형식으로 저장하고 불러올 수 있습니다.

> 추천하는 사용 방법은 아래와 같습니다.

디자이너가 배치한 특정 오브젝트가 맵에 존재하는 이미지를 제공해줍니다. 또는 이미지를 배치한 기획서를 제공합니다.

GPT 또는 이미지 인식이 가능한 인공지능 API를 사용하여 JSON 형식으로 상대 좌표를 가져옵니다.

또는 상대좌표를 직접 계산하는 방법도 있습니다.

계산된 좌표를 추가하고 Offset 설정을 바꿔가며 기획된 오브젝트 배치도와 비슷한지 확인하면서 만들면 됩니다!
