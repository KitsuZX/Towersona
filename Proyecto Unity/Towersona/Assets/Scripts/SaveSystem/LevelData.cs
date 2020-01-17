[System.Serializable]
public class LevelData
{
	public int index;
	public int score;
	public bool avaible;

	public bool Completed
    {
        get
        {
            return score != 0;
        }
    }

    public LevelData(int index, int score, bool available)
    {
        this.index = index;
        this.score = score;
        this.avaible = available;
    }   
}
