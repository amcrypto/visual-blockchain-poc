using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ardor;
using TMPro;

public class displayBlock : MonoBehaviour
{
    public TextMeshPro UI_previousBlockHash;
    public TextMeshPro UI_previousBlock;
    public TextMeshPro UI_block;
    public TextMeshPro UI_payloadHash;
    public TextMeshPro UI_numberOfTransactions;
    public TextMeshPro UI_generatorRS;
    public TextMeshPro UI_totalFeeFQT;

    public Ardor.Data.Block Block;
    public displayBlock(string s) 
    {

        Block = JsonUtility.FromJson<Ardor.Data.Block>(s);
        // Fill UI from block data
        UI_previousBlockHash.SetText(Block.previousBlockHash);
        UI_previousBlock.SetText(Block.previousBlock);
        UI_block.SetText(Block.block);
        UI_payloadHash.SetText(Block.payloadHash);
        UI_generatorRS.SetText(Block.generatorRS);
        UI_totalFeeFQT.SetText(Block.totalFeeFQT);
    }
    public void display(Ardor.Data.Block Block)
    {
        // Fill UI from block data
        UI_previousBlockHash.SetText(Block.previousBlockHash);
        UI_previousBlock.SetText(Block.previousBlock);
        UI_block.SetText(Block.block + "\n Block ID");
        UI_payloadHash.SetText(Block.payloadHash);
        UI_numberOfTransactions.SetText(Block.numberOfTransactions + "\n Transactions");
        UI_generatorRS.SetText(Block.generatorRS + "\n Generating Account");
        UI_totalFeeFQT.SetText(Block.totalFeeFQT+"\n Fee");
    }
}
