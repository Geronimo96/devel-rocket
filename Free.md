# 🔢 변수 (25.12.08)
> 과제 1. 동기들의 정보 출력하기
- 과정을 함께 듣는 동기 5명에 대한 정보를 조사하고, 출력하는 프로그램을 작성하시오.
- 인원 1명 당 각각 아래의 요소를 저장할 수 있도록 변수를 선언해야 한다. 데이터를 담기 위한 자료형은 적절하게 선택하도록 한다.

### 📄 지원자 정보 요약

| 이름 | 나이 | 키 (cm) | 지원 동기 |
| :--- | :--- | :--- | :--- |
| 김유훈 | 27 | 183.2 | 포트폴리오 완성도를 높이려고 지원했습니다. |
| 홍정옥 | 24 | 168.0 | 유튜브로 관심이 생겼는데 직접 게임까지 만들어보면 재밌을것같아서 지원했습니다. |
| 고병희 | 25 | 172.3 | 개발자가 멋있어보여서 지원하게 되었습니다. |
| 이인 | 만 24세 | 180.0 | 취직하고 싶어서 지원했습니다. |
| 김태형 | 28 | 177.0 | 취업 및 친구와 같이 게임을 만들고 싶어서 지원했습니다. |

---

```csharp
using System;

namespace _251209_Hello_World
{
    internal class Program
    {
        static void Main(string[] args)
        {   // 블록범위 시작

            string name1 = "이인"; string name2 = "김동현"; string name3 = "홍정옥"; string name4 = "안정연"; string name5 = "조규민";
            int age1 = 24; int age2 = 27; int age3 = 24; int age4 = 25; int age5 = 28;
            float stature1 = 180f; float stature2 = 173f; float stature3 = 168f; float stature4 = 170.1f; float stature5 = 171.3f;
            string motivation1 = "취직하고 싶어서 지원했습니다."; 
            string motivation2 = "게임회사에 취업하고싶어서 입니다."; 
            string motivation3 = "유튜브로 관심이 생겼는데 직접 게임까지 만들어보면 재밌을것같아서 지원했습니당."; 
            string motivation4 = "게임개발자가 꿈이어서 취업하기위해 입니다."; 
            string motivation5 = "평소에도 게임을 좋아하는데 우연히 이런 교육 프로그램이 있다는 사실을 알게 되어 도전하게 되었습니다.";

            Console.WriteLine($"· 이름 : {name1}\n· 나이 : {age1} 살\n· 키 : {stature1} cm\n· 지원 동기 : {motivation1}");
            Console.WriteLine($"· 이름 : {name2}\n· 나이 : {age2} 살\n· 키 : {stature2} cm\n· 지원 동기 : {motivation2}");
            Console.WriteLine($"· 이름 : {name3}\n· 나이 : {age3} 살\n· 키 : {stature3} cm\n· 지원 동기 : {motivation3}");
            Console.WriteLine($"· 이름 : {name4}\n· 나이 : {age4} 살\n· 키 : {stature4} cm\n· 지원 동기 : {motivation4}");
            Console.WriteLine($"· 이름 : {name5}\n· 나이 : {age5} 살\n· 키 : {stature5} cm\n· 지원 동기 : {motivation5}");

        }   // 블록범위 끝
    }
}





```









