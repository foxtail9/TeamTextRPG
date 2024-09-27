using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG;
public static class DungeonDatabase
{
    // 외부 수정 불가
    public static List<Dungeon> Dungeons { get; private set; }

    // 스태틱 생성자를 통해 시작 시 초기화를 진행함.

    // 던전
    static DungeonDatabase()
    {
        Dungeons = new List<Dungeon>
        {
            // ~ 난이도 초기화

            // ~ 출현 몬스터 정보 초기화

            // ~ 던전 설명 초기화

        };
    }

    // ~ 추가 기능 / 난이도 별 던전 가져오기
    // ~ 추가 기능 / 해당 몬스터 출현 던전 검색하기
}