namespace SchoolApp.Admin.Services.IntegrationEvents.Events;


public record ConfirmedOrderStockItem(int ProductId, bool HasStock);
