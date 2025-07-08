# GeminiAPI for C#

C#에서 Google Gemini API를 간편하게 사용할 수 있도록 만든 라이브러리입니다.  
비동기 방식으로 텍스트 요청을 처리하며, System Prompt를 통해 역할 기반 대화가 가능합니다.

---

## 🔧 사용 방법

### 1. API Key 발급

아래 링크에서 Google Gemini API Key를 발급받아야 합니다:  
👉 [https://aistudio.google.com/apikey](https://aistudio.google.com/apikey)

---

### 2. 코드 예제

```csharp
// 1. GeminiAPI 인스턴스 생성 (두 번째 인자는 시스템이 기억할 최대 채팅 수 - 기본값: 10)
var geminiAPI = new GeminiAPI("YOUR_API_KEY", 20);

// 2. 시스템 프롬프트 설정 (Gemini의 역할을 정의하는 부분)
geminiAPI.SetSystemPrompt("당신은 친절하고 유머러스한 물리학 선생님입니다. 매 답변마다 간단한 과학 상식을 하나 덧붙여 주세요.");

// 3. 사용자 메시지 보내기 및 응답 받기
string userMessage = "빛의 속도에 대해 설명해줘.";
string result = await geminiAPI.SendMessageAsync(userMessage);

Console.WriteLine(result);
```
---
## ⚙️ 개발 환경

- **.NET 8.0** (ASP.NET Core 8.0 포함)
- **Windows 환경 기준**

---

## 📦 사용된 패키지

- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json)  
  → `JsonConvert`를 이용한 JSON 직렬화 및 역직렬화에 사용

> 📌 NuGet으로 설치하려면:

```bash
dotnet add package Newtonsoft.Json

