
namespace Item_farm
{
    public enum _item_type
    {
        bubbly_meat = 0,
        sweat_juice,
        touch_light,
        talk_grass,
        none,
    }

    public struct Bubbly_meat
    {
        public float Effect_time{ get; set; }
        public float Limit_time { get; set; }
        public Bubbly_meat(float effect, float limit)
        {
            Effect_time = effect;
            Limit_time = limit;
        }
    }

    public struct Sweat_juice
    {
        public float Effect_time{ get; set; }
        public float Limit_time { get; set; }
        public Sweat_juice(float effect, float limit)
        {
            Effect_time = effect;
            Limit_time = limit;
        }
    }
    public struct Touch_light
    {
        public float Effect_time{ get; set; }
        public float Limit_time { get; set; }
        public Touch_light(float effect, float limit)
        {
            Effect_time = effect;
            Limit_time = limit;
        }
    }
    public struct Talk_grass
    {
        public float Effect_time{ get; set; }
        public float Limit_time { get; set; }
        public Talk_grass(float effect, float limit)
        {
            Effect_time = effect;
            Limit_time = limit;
        }
    }

}
