using System;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzle : MonoBehaviour
{
    private const int Size = 3;

    [Header("Tile Prefabs")]
    [SerializeField] private PipeTile straightPrefab;
    [SerializeField] private PipeTile cornerPrefab;

    [Header("Layout (S = straight, C = corner, rows separated by /)")]
    [SerializeField] private string layout = "SSC/CSS/SCC";

    public event Action Solved;

    private readonly PipeTile[,] grid = new PipeTile[Size, Size];
    private bool built;
    private bool active;
    private bool done;

    private static readonly Vector2Int[] Offsets =
    {
        new Vector2Int(-1, 0), new Vector2Int(0, 1),
        new Vector2Int(1, 0),  new Vector2Int(0, -1)
    };

    private void Awake()
    {
        Build();
    }

    private void Build()
    {
        if (built) return;
        built = true;

        string[] rows = layout.Split('/');

        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                PipeTile prefab = rows[r][c] == 'C' ? cornerPrefab : straightPrefab;
                PipeTile tile = Instantiate(prefab, transform);
                tile.Clicked += OnTileClicked;
                grid[r, c] = tile;
            }
        }

        do
        {
            foreach (PipeTile tile in grid)
                tile.SetRotation(UnityEngine.Random.Range(0, 4));
        }
        while (Evaluate(out _));
    }

    public void Begin()
    {
        Build();
        if (done) return;
        active = true;
        Refresh();
    }

    public void End()
    {
        active = false;
    }

    private void OnTileClicked(PipeTile tile)
    {
        if (!active || done) return;
        tile.RotateClockwise();
        Refresh();
    }

    private void Refresh()
    {
        bool solved = Evaluate(out bool[,] powered);

        for (int r = 0; r < Size; r++)
            for (int c = 0; c < Size; c++)
                grid[r, c].SetPowered(powered[r, c]);

        if (solved && !done)
        {
            done = true;
            Solved?.Invoke();
        }
    }


    private bool Evaluate(out bool[,] powered)
    {
        powered = new bool[Size, Size];

        if (!grid[0, 0].Has(3)) return false; 

        var stack = new Stack<Vector2Int>();
        stack.Push(Vector2Int.zero);
        powered[0, 0] = true;

        while (stack.Count > 0)
        {
            Vector2Int p = stack.Pop();
            PipeTile tile = grid[p.x, p.y];

            for (int dir = 0; dir < 4; dir++)
            {
                if (!tile.Has(dir)) continue;

                Vector2Int next = p + Offsets[dir];
                if (next.x < 0 || next.x >= Size || next.y < 0 || next.y >= Size) continue;
                if (powered[next.x, next.y]) continue;
                if (!grid[next.x, next.y].Has((dir + 2) % 4)) continue;

                powered[next.x, next.y] = true;
                stack.Push(next);
            }
        }

        return powered[Size - 1, Size - 1] && grid[Size - 1, Size - 1].Has(1);
    }
}