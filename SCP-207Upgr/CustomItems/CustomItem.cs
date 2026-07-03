using LabApi.Features.Wrappers;

internal abstract class CustomItem
{
    public abstract string Name { get; }

    public abstract ItemType BaseItem { get; }

    public abstract void OnUse(Player player, Item item);
}