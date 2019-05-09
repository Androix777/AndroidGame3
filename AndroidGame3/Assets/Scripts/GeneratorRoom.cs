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
    public GameObject RoomPref;
    public TextAsset patternsText;
    List<int[,]> patterns = new List<int[,]>();  
    public TextAsset blockText;
    List<Block>[] blocks = new List<Block>[11];
    int[] Distribution = new int[] { 5, 15, 60, 15, 5 };

    int[,] Generpattern;
    Block[] Generblocks;
    private void Awake()
    {

        loadPatterns();
        loadBlocks();
    }


    public void GenerationRoom(int LvlRoom)
    {
        
        int difficult = difficultF(LvlRoom);        
        Generpattern = generationPattern(difficult);        
        int[] difficultBlocks = generationDifficultBlocks(Generpattern, difficult);        
        Generblocks = generationBlocks(difficultBlocks, Generpattern);       
        CreateRoom(Generblocks, Generpattern);

    }
    public void LoadLastRoom()
    {
        CreateRoom(Generblocks, Generpattern);
    }
    public void CreateRoom(Block[] blocks,int[,] pattern)
    {
        if (Room != null) Destroy(Room);
        GameObject roomObj = Instantiate(RoomPref) as GameObject;
        roomObj.transform.position = new Vector3(0 , 0.1f , 0);
        roomObj.tag = "Room";
        Room = roomObj;
        for (int i = 0; i< blocks.Length;i++)
        {
            //Debug.Log("Blocks/" + blocks[i].objBlock + " " + blocks[i].length + " " + blocks[i].width + " " + blocks[i].difficult);
            GameObject blockObj = Instantiate(Resources.Load("Blocks/" + blocks[i].objBlock)) as GameObject;
            blockObj.transform.SetParent(Room.transform);
            //Debug.Log((int)(Math.Abs(pattern[0, i] + pattern[2, i]) / 2)+" "+ 0 + " " + (int)(Math.Abs(pattern[1, i] + pattern[3, i]) / 2));
            blockObj.transform.localPosition = new Vector3((int)(Math.Abs(pattern[0, i] + pattern[2, i]) / 2 -8), 0, (int)(Math.Abs(pattern[1, i] + pattern[3, i]) / 2 - 8));
        }  
    }

    int difficultF(int LvlRoom)
    {
        
        double difficulttRoom = Math.Pow(Math.Pow((LvlRoom + 1) / 2, 1 / 3) * 5,2);
        return (int)difficulttRoom;
    }

    int[,] generationPattern(int difficult)
    {
        
        return patterns[UnityEngine.Random.Range(0,patterns.Count)];
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
            for (int j = diffMin; j <= diffMax; j++)
            {
                
                Block[] AddingBlock = findBlocksSize(sizeBlock[1], sizeBlock[0], j);
                listarraysBlocks.Add(AddingBlock);
                summBlocks += AddingBlock.Length;
            }

            /*if (summBlocks == 0)
            {
                Debug.Log("Zero " + sizeBlock[1] + " " + sizeBlock[0] + " " + diff);
            }
            for (int j = 0; j <= diffMax - diffMin; j++)
            {
                Debug.Log("Block " + sizeBlock[1] + " " + sizeBlock[0] + " " + listarraysBlocks[j].Length + " " +j);
            }*/

            float[] percentBlocks = new float[5];
            float summDistributionBlocks = 0;
            for (int j = 0; j <= diffMax - diffMin; j++)
            {
                int dist = Distribution[j];
                if (diff == 0)
                {                    
                    dist = Distribution[j + 2];
                }
                if (diff == 1)
                {
                    dist = Distribution[j + 1]; ;
                }
                if (summBlocks != 0) percentBlocks[j] = listarraysBlocks[j].Length * 100f * dist / summBlocks ;
                else percentBlocks[j] = 0;
                //Debug.Log((listarraysBlocks[j].Length / summBlocks * 100f * dist) + " " + listarraysBlocks[j].Length / summBlocks);
                summDistributionBlocks += percentBlocks[j];
                //Debug.Log("PercentBlocks " + percentBlocks[j]);
            }
            
            float[] chance = new float[5];
            for (int j = 0; j <= diffMax - diffMin; j++)
            {
                
                chance[j] = percentBlocks[j] / (summDistributionBlocks + 1f) * 100f;
                
            }

            
            int rand = UnityEngine.Random.Range(1,100);
            for (int j = 0; j <= diffMax - diffMin; j++)
            {
                
                
                
                rand -= (int)Math.Ceiling(chance[j]);
                
                if ((int)rand <= 0 || j == diffMax - diffMin)
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

        diff = (int)Math.Sqrt(difficult * 4 / pattern.Length);
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
            if ((block.length == length  || block.length + 1 == length ) && (block.width == width  || block.width + 1 == width ))
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
                //Debug.Log(pattern.Length / 4 + " " + lines.Length + " " + patternsString.Length);
                string[] splitLine = lines[j].Split(' ');
                pattern[0, j] = int.Parse(splitLine[0]);
                pattern[1, j] = int.Parse(splitLine[1]);
                pattern[2, j] = int.Parse(splitLine[2]) + 1;
                pattern[3, j] = int.Parse(splitLine[3]) + 1;
            }
            patterns.Add(pattern);
        }
       
    }

    void loadBlocks()
    {
        string alltext = blockText.text;

        string[] lines = alltext.Split('\n');

        for(int i = 0; i < 11; i++)
        {
            blocks[i] = new List<Block>();
        }
        for (int i = 0; i < lines.Length; i++)
        {
            string[] splitLine = lines[i].Split(' ');

            
            if (splitLine.Length == 4)
            {
                //Debug.Log(splitLine[0] + " " + splitLine[1] + " " + splitLine[2] + " " + splitLine[3]);
                blocks[int.Parse(splitLine[3])].Add(new Block(splitLine[0], int.Parse(splitLine[1]), int.Parse(splitLine[2]), int.Parse(splitLine[3])));
            }
            

        }
        //Debug.Log("");
    }

    /*static void SaveRoom(Block[] data,int[] pattern, int characterSlot)
    {
        for

        PlayerPrefs.SetInt("LastRoom" + characterSlot, data.Room);
        PlayerPrefs.SetInt("Music" + characterSlot, data.MusicActive);
        PlayerPrefs.Save();
    }

    static PesronData LoadBlocks(int characterSlot)
    {
        PesronData loadedCharacter = new PesronData();
        loadedCharacter.Room = PlayerPrefs.GetInt("LastRoom" + characterSlot);
        loadedCharacter.MusicActive = PlayerPrefs.GetInt("Music" + characterSlot);
        return loadedCharacter;
    }
    static PesronData LoadPattern(int characterSlot)
    {
        PesronData loadedCharacter = new PesronData();
        loadedCharacter.Room = PlayerPrefs.GetInt("LastRoom" + characterSlot);
        loadedCharacter.MusicActive = PlayerPrefs.GetInt("Music" + characterSlot);
        return loadedCharacter;
    }*/
}
