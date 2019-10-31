using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleGrapher : MonoBehaviour {

	public int scale = 500;
	public int xoffset = 100;
	public int yoffset = 100;
	RawImage render;
	Texture2D texture;
	Texture2D graph;
	Color[] colours;

	void Circle (int cx, int cy, int r, Color col) 
	{
        texture = GetComponent<RawImage>().texture as Texture2D;
	    int y = r;
	    float d = 1/4.0f - r;
	    float end = Mathf.Ceil(r/Mathf.Sqrt(2));
	   
	    for (int x = 0; x <= end; x++) {
	        texture.SetPixel(cx+x, cy+y, col);
	        texture.SetPixel(cx+x, cy-y, col);
	        texture.SetPixel(cx-x, cy+y, col);
	        texture.SetPixel(cx-x, cy-y, col);
	        texture.SetPixel(cx+y, cy+x, col);
	        texture.SetPixel(cx-y, cy+x, col);
	        texture.SetPixel(cx+y, cy-x, col);
	        texture.SetPixel(cx-y, cy-x, col);
	       
	        d += 2*x+1;
	        if (d > 0) {
	            d += 2 - 2*y--;
	        }
	    }
	}

	public void DrawLine(float x, float y, float x2, float y2, Color c)
	{
		x = x * scale + xoffset;
		y = y * scale + yoffset;
		x2 = x2 * scale + xoffset;
		y2 = y2 * scale + yoffset;
		Circle((int)x,(int)y,10,Color.red);
		Circle((int)x2,(int)y2,10,Color.red);

		int weight = (int)(x2 - x);
	    int height = (int)(y2 - y);
	    int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0 ;
	    if (weight<0) dx1 = -1 ; else if (weight>0) dx1 = 1 ;
	    if (height<0) dy1 = -1 ; else if (height>0) dy1 = 1 ;
	    if (weight<0) dx2 = -1 ; else if (weight>0) dx2 = 1 ;
	    int longest = Mathf.Abs(weight) ;
	    int shortest = Mathf.Abs(height) ;
	    if (!(longest>shortest)) {
	        longest = Mathf.Abs(height) ;
	        shortest = Mathf.Abs(weight) ;
	        if (height<0) dy2 = -1 ; else if (height>0) dy2 = 1 ;
	        dx2 = 0 ;            
	    }
	    int numerator = longest >> 1 ;
	    for (int i=0;i<=longest;i++) {
	        texture.SetPixel((int)x,(int)y,c) ;
	        numerator += shortest ;
	        if (!(numerator<longest)) {
	            numerator -= longest ;
	            x += dx1 ;
	            y += dy1 ;
	        } else {
	            x += dx2 ;
	            y += dy2 ;
	        }
	    }
        texture.Apply();
		render.texture = texture;
	}

	public void DrawRay(float slope, float intercept, Color c)
	{
		//y = mx + c
		float x = 0 + xoffset;
		float y = (intercept * scale) + yoffset;

		float x2 = 600 + xoffset;
		float y2 = slope * x2 + (intercept * scale) + yoffset;

		//Circle((int)x,(int)y,10,Color.green);
		//Circle((int)x2,(int)y2,10,Color.green);

		int weight = (int)(x2 - x);
	    int height = (int)(y2 - y);
	    int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0 ;
	    if (weight<0) dx1 = -1 ; else if (weight>0) dx1 = 1 ;
	    if (height<0) dy1 = -1 ; else if (height>0) dy1 = 1 ;
	    if (weight<0) dx2 = -1 ; else if (weight>0) dx2 = 1 ;
	    int longest = Mathf.Abs(weight) ;
	    int shortest = Mathf.Abs(height) ;
	    if (!(longest>shortest)) {
	        longest = Mathf.Abs(height) ;
	        shortest = Mathf.Abs(weight) ;
	        if (height<0) dy2 = -1 ; else if (height>0) dy2 = 1 ;
	        dx2 = 0 ;            
	    }
	    int numerator = longest >> 1 ;
	    for (int i=0;i<=longest;i++) {
	        texture.SetPixel((int)x,(int)y,c) ;
	        numerator += shortest ;
	        if (!(numerator<longest)) {
	            numerator -= longest ;
	            x += dx1 ;
	            y += dy1 ;
	        } else {
	            x += dx2 ;
	            y += dy2 ;
	        }
	    }
        texture.Apply();
		render.texture = texture;
	}

	public void DrawPoint(float x, float y, Color c)
	{
		Circle((int)(x*scale)+xoffset,(int)(y*scale)+yoffset,5,c);
		texture.Apply();
		render.texture = texture;
	}

	void DrawAxis(Color c)
	{
		for(int x = 0; x < 600; x++)
			texture.SetPixel(x, yoffset, c);
		for(int y = 0; y < 600; y++)
			texture.SetPixel(xoffset, y, c);
	}

    public void Init()
    {
        render = GetComponent<RawImage>();
        texture = render.texture as Texture2D;

        colours = texture.GetPixels(0);
        for (int i = 0; i < colours.Length; i++)
            colours[i] = Color.black;

        texture.SetPixels(colours, 0);
        DrawAxis(Color.white);

        texture.Apply();
        render.texture = texture;
    }

	// Use this for initialization
	void Start () {
		
	}
}
