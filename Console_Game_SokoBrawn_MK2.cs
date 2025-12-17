using System;
using System.Text;

class Program
{
    private const char PLAYER = 'P';
    private const char PLAYER_ON_GOAL = '@';
    private const char BOMB = 'B';
    private const char BOMB_ON_GOAL = '!';
    private const char GOAL = 'G';
    private const char WALL = '#';
    private const char EMPTY = ' ';

    // [변경] 원본 맵 데이터 (변하지 않음, 읽기 전용)
    // 게임을 리셋할 때 이 데이터를 복사해서 사용합니다.
    private static readonly char[,] _originalMap = new char[,]
    {
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', 'B', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', 'G', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', 'P', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', 'G', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', 'B', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
    };

    // [변경] 실제 게임에서 사용할 맵 변수 (초기화 시 원본에서 복사됨)
    private static char[,] map;

    // [변경] 시작 위치 저장 (리셋용)
    static readonly Position _startPos = new Position() { X = 4, Y = 4 };

    // [변경] 현재 플레이어 위치
    static Position _playerPos;

    private static int _moveCount = 0;

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        // [변경] 게임 시작 전 최초 1회 초기화 실행
        ResetGame();

        PrintGuideText();

        while (true)
        {
            PrintMoveCount();
            PrintMap();

            if (IsGameClear())
            {
                PrintClearText();
                break;
            }

            ConsoleKey inputKey;
            if (!TryGetInput(out inputKey)) continue;

            if (inputKey == ConsoleKey.Q)
            {
                Console.WriteLine("\n게임을 종료합니다");
                break;
            }

            // [변경] R키 입력 시 리셋 로직 실행
            if (inputKey == ConsoleKey.R)
            {
                ResetGame();
                continue; // 리셋 후 루프의 처음(화면 출력)으로 돌아감
            }

            // --- 이하 로직 동일 ---

            Position nextPos = GetNextPosition(inputKey);

            if (IsOutOfArray(nextPos)) continue;

            char targetTile = GetTile(nextPos);
            if (targetTile == WALL) continue;

            if (targetTile == EMPTY || targetTile == GOAL)
            {
                Move(_playerPos, nextPos, PLAYER);
                _playerPos = nextPos;
                _moveCount++;
            }
            else if (targetTile == BOMB || targetTile == BOMB_ON_GOAL)
            {
                if (TryPushBomb(nextPos))
                {
                    _playerPos = nextPos;
                    _moveCount++;
                }
            }
        }

        Console.WriteLine("게임 끝");
    }

    // [변경] 게임 상태를 초기화하는 함수 추가
    static void ResetGame()
    {
        // 1. 맵 복사 (Clone을 사용하여 깊은 복사 처리)
        // 원본(_originalMap)은 건드리지 않고 map에 새로 덮어씁니다.
        map = (char[,])_originalMap.Clone();

        // 2. 플레이어 위치 초기화
        _playerPos = _startPos;

        // 3. 이동 횟수 초기화
        _moveCount = 0;

        // 4. 화면 지우기 (잔상 제거용)
        Console.Clear();
        PrintGuideText(); // 가이드 텍스트 다시 출력
    }

    static void PrintGuideText()
    {
        Console.Clear();
        // [변경] 안내 문구에 R 추가
        Console.WriteLine("W/A/S/D : 이동 | R : 리셋 | Q : 종료");
        Console.WriteLine("모든 폭탄을 목표지점으로 옮기세요");
        Console.WriteLine();
    }

    static void PrintMoveCount()
    {
        Console.SetCursorPosition(0, 4);
        Console.WriteLine($"이동 거리  : {_moveCount}");
        Console.WriteLine();
    }

    // ... (IsGameClear, PrintClearText 함수는 동일) ...
    static bool IsGameClear()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == BOMB || map[y, x] == GOAL) return false;
            }
        }
        return true;
    }

    static void PrintClearText()
    {
        Console.WriteLine();
        Console.WriteLine("축하합니다. 클리어 하셨습니다");
        Console.WriteLine($"총 이동 거리 : {_moveCount}");
        Console.WriteLine();
    }

    static bool TryGetInput(out ConsoleKey inputKey)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        inputKey = keyInfo.Key;

        return inputKey == ConsoleKey.W ||
               inputKey == ConsoleKey.A ||
               inputKey == ConsoleKey.S ||
               inputKey == ConsoleKey.D ||
               inputKey == ConsoleKey.Q ||
               inputKey == ConsoleKey.R; // [변경] R키 허용
    }

    // ... (이하 나머지 함수들은 기존 코드와 100% 동일하여 생략하거나 그대로 둡니다) ...

    static Position GetNextPosition(ConsoleKey inputKey)
    {
        int newX = _playerPos.X;
        int newY = _playerPos.Y;

        if (inputKey == ConsoleKey.W) newY--;
        else if (inputKey == ConsoleKey.S) newY++;
        else if (inputKey == ConsoleKey.A) newX--;
        else if (inputKey == ConsoleKey.D) newX++;

        return new Position() { X = newX, Y = newY };
    }

    static char GetTile(Position pos) => map[pos.Y, pos.X];
    static void SetTile(Position pos, char tile) => map[pos.Y, pos.X] = tile;

    static bool IsOutOfArray(Position pos)
    {
        bool outX = pos.X < 0 || map.GetLength(1) <= pos.X;
        bool outY = pos.Y < 0 || map.GetLength(0) <= pos.Y;
        return outX || outY;
    }

    static void Move(Position from, Position to, char target)
    {
        char originTile = GetOriginTile(GetTile(from));
        SetTile(from, originTile);
        char targetTile = GetTile(to);
        char nextTile = GetConvertTile(target, targetTile);
        SetTile(to, nextTile);
    }

    static char GetConvertTile(char mover, char under)
    {
        if (mover == PLAYER)
            return (under == GOAL) ? PLAYER_ON_GOAL : PLAYER;
        else if (mover == BOMB)
            return (under == GOAL) ? BOMB_ON_GOAL : BOMB;
        return under;
    }

    static char GetOriginTile(char tile)
    {
        switch (tile)
        {
            case PLAYER: return EMPTY;
            case PLAYER_ON_GOAL: return GOAL;
            case BOMB: return EMPTY;
            case BOMB_ON_GOAL: return GOAL;
            default: return tile;
        }
    }

    static bool TryPushBomb(Position bombPos)
    {
        Position direction = GetDirection(_playerPos, bombPos);
        Position nextPos = AddDirection(bombPos, direction);

        if (IsOutOfArray(nextPos)) return false;
        char nextTile = GetTile(nextPos);
        if (!(nextTile == EMPTY || nextTile == GOAL)) return false;

        Move(bombPos, nextPos, BOMB);
        Move(_playerPos, bombPos, PLAYER);
        return true;
    }

    static Position GetDirection(Position from, Position to)
    {
        return new Position() { X = to.X - from.X, Y = to.Y - from.Y };
    }

    static Position AddDirection(Position pos, Position direction)
    {
        return new Position() { X = pos.X + direction.X, Y = pos.Y + direction.Y };
    }

    static void PrintMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                char tile = map[i, j];
                if (tile == WALL) Console.Write("🧱");
                else if (tile == PLAYER) Console.Write("🤔");
                else if (tile == PLAYER_ON_GOAL) Console.Write("😵");
                else if (tile == BOMB) Console.Write("💣");
                else if (tile == BOMB_ON_GOAL) Console.Write("✅");
                else if (tile == GOAL) Console.Write("🕳️");
                else Console.Write("  ");
            }
            Console.WriteLine();
        }
    }
}

public struct Position
{
    public int X;
    public int Y;
}