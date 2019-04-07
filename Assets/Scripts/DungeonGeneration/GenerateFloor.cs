using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateFloor : MonoBehaviour {
    public int radius = 50;
    public int amountOfCells = 50;
    public int mean = 6, stdDev = 2;
    [Range(1.0f, 2.0f)] public float margin = 1.25f;

    private Rect[] cells;
    private Rect[] x_i; //rooms

    private bool cellsSeparated = false;
    
    //event when map is finished
    public delegate void OnGenerationFinished();
    public OnGenerationFinished onGenerationFinishing;

    private void Start() {
        cells = new Rect[amountOfCells];
        for(var i = 0; i < cells.Length; i++) {
            cells[i] = CreateRectangle(radius, mean, stdDev);
        }
    }

    private IEnumerator Generate() {
        SepparateCells(cells);
        x_i = SetRooms(cells, mean, margin);
        yield return new WaitForSeconds(1000);
        Debug.Log("Generation finished");
    }

    private void OnDrawGizmos() {
        if(cells == null) return;
        
        var treshhold = mean * margin;
        
        foreach (Rect r in cells) {
            if (r.width >= treshhold || r.height >= treshhold) {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(r.center, r.size);
            } else {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(r.center, r.size);
            }
        }
    }

    private Rect CreateRectangle(int radius, int mean, int stdDev) {
        Rect r = new Rect {
            center = GetPointWithinRadius(radius),
            size = GetRandomSize(mean, stdDev)
        };
        return r;
    }

    private Vector2 GetRandomSize(int mean, int stdDev) {
        var xscale = Mathf.RoundToInt(NormalDistribution(mean, stdDev));
        var yscale = Mathf.RoundToInt(NormalDistribution(mean, stdDev));
        return new Vector2(xscale, yscale);
    }

    private float NormalDistribution(int mean, int stdDev) {
        var u1 = 1.0f - Random.Range(0f, 1f);
        var u2 = 1.0f - Random.Range(0f, 1f);
        
        var randStdNormal = 
            Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
            Mathf.Sin(2.0f * Mathf.PI * u2);
        
        return mean + stdDev * randStdNormal;
    }

    private Vector2 GetPointWithinRadius(int radius) {
        int xpos, ypos;

		var r = radius * Mathf.Sqrt (Random.Range (0f, 1f));
		var theta = Random.Range (0f, 1f) * 2 * Mathf.PI;
		xpos = Mathf.RoundToInt(r * Mathf.Cos (theta));
		ypos = Mathf.RoundToInt(r * Mathf.Sin (theta));
        return new Vector2(xpos, ypos);
    }

    private void SepparateCells(Rect[] cells) {
		while (!cellsSeparated) {
            cellsSeparated = true;
			for (var seed = 0; seed < cells.Length; seed++) { 
				for (var r = seed + 1; r < cells.Length; r++) {
                    if (!cells[r].Overlaps(cells[seed])) continue;
                    var direction = new Vector2 (
                        cells [r].center.x - cells [seed].center.x,
                        cells [r].center.x - cells [seed].center.x
                    );
                    //direction *= -1;
                    cells[r].center += 1 * direction.normalized;
                    cellsSeparated = false;
                }
			}
		}
    }

    private Rect[] SetRooms(Rect[] rectArray, int median, float percent) {
        var treshhold = median * percent;
        return rectArray.Where(r => r.width >= treshhold || r.height >= treshhold).ToArray();
    }

    

    private Rect[] SortArray(Rect[] x, Rect seed, int index) {
        Rect temp;
        for(var i = index; i < x.Length; i++) {
            for(var j = index; j < x.Length - 1; j++) {
                if (!(Vector2.Distance(seed.center, x[j].center) >
                      Vector2.Distance(seed.center, x[j + 1].center))) continue;
                temp = x[j];
                x[j] = x[j + 1];
                x[j + 1] = temp;
            }
        }
        return x;
    }
}
