# Multiplay_Beta ( 1. 깃 허브 업로드, 2. 프로젝트 환경 설정 )
## 1. 깃허브 업로드


### Git 기본 세팅
1.Git bash를 통해 git init 실행<br>
2.Git remote add origin "원격 저장소 주소"

---
<br/>

### Git 대용량 파일 전송 기능 ( For Firebase )
3.Git lfs install <h6> 대용량 파일 전송 기능. https://git-lfs.com</h6> <br>
4.git lfs track "대용량 폴더 혹은 파일"<br>
 <h6> 대용량 파일이 다 Firebase 폴더에 있어서 Firebase폴더 경로로 추적 시키면 됨</h6>
  
<br/>
<br/>


5.git add .gitattributes<br>
6.git commit -m "메시지"<br>
7.git push origin master<br>
  <h6>먼저 .gitattributes 부터 무조건 푸쉬 해줘야 함. 저기에 Tracking 정보 다 있음</h6>
<br/>
---
<br/>

## CRLF Controller

이제 본격적으로 Push를 하려고 add 부터 하려는데, <br>
아마 LF CRLF 뭐시기 하면서, git add . 하면 오류 생길 것임.

8. git config core.autocrlf true

  <h6>이건 에셋 제작자마다 (Mac,Window,Linux 등) OS가 다르기 때문에 문자열 통일이 안돼서 일어나는 오류
    git config core.autocrlf true 때리면 됨 
    git이 이 부분마저 처리해준다는 뜻 </h6>

9. 부터는 git add . 하고 원래 커밋 하던데로 하면 
- git add .
- git commit -m "~~~"
- git push origin master

<br/>

주의 사항
- 8번 부터는 파일 변경 있을대 마다 계속 해줘야 됨.
- 그리고 첫 커밋 후 부턴 (간단히 말하면 지금 부터는) 커밋 하기전에 무조건 <br> **Git pull origin master** 해주기 ( 원격 저장소와 로컬 저장소 동기화 )


---
<br/>

## 2. 프로젝트 환경설정 및 안내

- 현재 폴더 잡다하게 많아서 작업하면 되는 Scene
  - Assets/Scene 에 있는 SignIn 혹은 Lobby Scene
- 프로젝트 용량 감소
- 안드로이드 빌드 플랫폼으로 돼 있음 ( 테스트를 위해 PC 개발 환경 플랫폼으로 전환 가능 File > Build Settings > Window,Mac,Linux )
- 해당 유니티 버전으로 안드로이드로 빌드 할 시 OpenJDK 1.8.0_152 버전 설치 필요
- Android API 30~33 ( https://note0913.tistory.com/328 )
- JAVA HOME 경로 설정 수정 필수 ( https://note0913.tistory.com/328 )
- Firebase 연동 Key 입력 필요
  - Edit > Project Settings > Player > Publish Settings ( Android 로고 눌러야 Publish Settings 존재 )
  - Custom Keystore 밑 Select 누르고 Assets 폴더 들어가서 user.keystore 선택
  - Alias : capstone7
  - password : ft6cy4zd@ ( 위 아래 둘다 )
- Photon 연결
  - 상단에 Tools 탭 > Fusion > Fusion hub > Welcome에 App ID
  - 779fc2a2-64ed-48b1-b61c-5aab4b2fff77 를 입력
