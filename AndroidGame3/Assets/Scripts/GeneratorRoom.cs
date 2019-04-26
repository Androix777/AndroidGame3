using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GeneratorRoom : MonoBehaviour
{
    public struct Block
    {
       public string objBlock;
       public int length, width, difficult;

       public Block(string objBlock, int length, int width, int difficult )
       {
            this.objBlock = objBlock;
            this.length = length;
            this.width = width;
            this.difficult = difficult;
       }

    }
    

    public int[] sizeX;
    public int[] sizeY;
    int[,] map = new int[14, 14];
    
    public GameObject Room;

    public TextAsset patternsText;
    List<int[,]> patterns = new List<int[,]>();

    public TextAsset blockText;
    List<Block>[] blocks = new List<Block>[10];
    int[] Distribution = new int[] { 5, 15, 60, 15, 5 };
    private void Start()
    {

        loadPatterns();
        loadBlocks();
    }


    public void GenerationRoom(int LvlRoom)
    {
        int difficult = difficultF(LvlRoom);

         

        List<GameObject> obj = new List<GameObject>();

        

    }

    public void CreateRoom()
    {

    }

    int difficultF(int LvlRoom)
    {
        
        double difficulttRoom = Math.Pow(Math.Pow((LvlRoom + 1) / 2, 1 / 3) * 5,2);
        return (int)difficulttRoom;
    }

    int[,] generationPattern(int difficult)
    {
        return new int[0,0];
    }

    public Block[] generationBlocks(int[] difficultsBlock,int[,] pattern)
    {
        Block[] blockRoom = new Block[difficultsBlock.Length];

        for (int i = 0;i < difficultsBlock.Length;i++)
        {
            int diff = difficultsBlock[i];
            int diffMax;
            int diffMin;
            if (diff + 2 > 10) diffMax = 10;
            else diffMax = diff + 2;
            if (diff - 2 < 0) diffMin = 0;
            else diffMin = diff - 2;

            int[] sizeBlock = new int[] { Math.Abs(pattern[0, i] - pattern[2, i]), Math.Abs(pattern[1, i] - pattern[3, i]) };
            

            List<Block[]> listarraysBlocks = new List<Block[]>();
            int summBlocks = 0;
            for (int j = diffMin; j < diffMax; j++)
            {
                Block[] AddingBlock = findBlocksSize(sizeBlock[1], sizeBlock[0], j);
                listarraysBlocks.Add(AddingBlock);
                summBlocks += AddingBlock.Length;
            }

            float[] percentBlocks = new float[5];
            float summDistributionBlocks = 0;
            for (int j = 0; j < 5; j++)
            {
                percentBlocks[j] = listarraysBlocks[j].Length / (100f * summBlocks) * Distribution[j];
                summDistributionBlocks += percentBlocks[j];
            }

            float[] chance = new float[5];
            for (int j = 0; j < 5; j++)
            {
                chance[j] = percentBlocks[i] / summDistributionBlocks * 100f;
            }
            
            float rand = UnityEngine.Random.Range(0f,100f);
            for (int j = 0; j < 5; j++)
            {
                rand -= chance[j];
                if (rand <= 0 || j == 4)
                {
                    blockRoom[i] = listarraysBlocks[j][UnityEngine.Random.Range(0, listarraysBlocks[j].Length)];
                    break;
                }
                
            }
        }

        return blockRoom;
    }

    public int[] generationDifficultBlocks(int[,] pattern,int difficult)
    {
        int diff;

        diff = (int)Math.Sqrt(difficult / pattern.Length);
        if (diff > 10)
        {
            diff = 10;
        }
        if (diff < 1)
        {
            diff = 1;
        }
        int[] difficultArray = new int[(int)pattern.Length / 4];
        for (int i = 0;i < pattern.Length/4f;i++)
        {
            
            difficultArray[i] = diff;

        }

        return difficultArray;
    }

    Block[] findBlocksSize(int length, int width, int difficult)
    {
        List<Block> blocksRet = new List<Block>();
        foreach (Block block in blocks[difficult])
        {
            if ((block.length == length || block.length + 1 == length) && (block.width == width || block.width + 1 == width))
            {
                blocksRet.Add(block);
            }
            
        }
        return blocksRet.ToArray();
    }

    void loadPatterns()
    {
        string alltext = patternsText.text;
        
        string[] patternsString = alltext.Split(new string[] { "\r\n\r\n" } , StringSplitOptions.None);

        for (int i = 0; i < patternsString.Length; i++)
        {
            string[] lines = patternsString[i].Split('\n');
            int[,] pattern = new int[4, lines.Length];

            for (int j = 0; j < lines.Length; j++)
            {
                string[] splitLine = lines[i].Split(' ');
                pattern[0, j] = int.Parse(splitLine[0]);
                pattern[1, j] = int.Parse(splitLine[1]);
                pattern[2, j] = int.Parse(splitLine[2]);
                pattern[3, j] = int.Parse(splitLine[3]);
            }
            patterns.Add(pattern);
        }
       
    }

    void loadBlocks()
    {
        string alltext = blockText.text;

        string[] lines = alltext.Split('\n');

        
        for (int i = 0; i < lines.Length; i++)
        {
            string[] splitLine = lines[i].Split(' ');

            blocks[int.Parse(splitLine[3])].Add(new Block(splitLine[0],int.Parse(splitLine[1]), int.Parse(splitLine[2]), int.Parse(splitLine[3])));
        }
        Debug.Log("");
    }

}
