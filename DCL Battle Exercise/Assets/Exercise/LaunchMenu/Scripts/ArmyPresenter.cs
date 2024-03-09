public interface IArmyPresenter
{
    void UpdateUnit(UnitType unitType, int amount);
    void UpdateStrategy(ArmyStrategy strategy);
}

public class ArmyPresenter : IArmyPresenter
{
    private readonly IArmyModel model;
    private readonly IArmyView view;

    public ArmyPresenter(IArmyModel model, IArmyView view)
    {
        this.model = model;
        this.view = view;
        this.view.UpdateWithModel(model);
    }

    public void UpdateUnit(UnitType unitType, int amount)
    {
        for (int i = 0; i < model.armyData.Length; i++)
        {
            if (model.armyData[i].unitType == unitType)
            {
                model.armyData[i].amount = amount;
            }
        }
    }

    public void UpdateStrategy(ArmyStrategy strategy)
    {
        model.strategy = strategy;
    }
}