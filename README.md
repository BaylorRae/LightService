# Light Service in C# 
This project has been written by a Ruby developer with no experience in C#. I
wanted to learn C# and thought this would be a fun way to introduce myself to
the language.

I watched Attila Domokos' talk on [Simple and Elegant Rails Code with Functional
Style] where he introduced [Light Service]. The concept immediately captured my
interest and I thought it would be a fun exercise to rewrite it in C#.

## Things worth noting

- It supports static contexts which result in compile errors and auto
  completion.
- Logic can be spread out into multiple classes to simplify code and ease
  testing.

## Example

```c#

// CONTROLLER
namespace LightServiceDemo.Controllers {
  public class OrdersController : Controller {
    public ActionResult Index() {

      // create order and calculate its tax
      Models.Order order = new Models.Order() { SubTotal = 55 };
      Contexts.OrderContext result = Organizers.CalculatesOrderTax.ForOrder(order);

      ViewBag.Order = result.Order;
      return View();

    }
  }
}

// CONTEXT
namespace LightServiceDemo.Contexts {
  public class OrderContext : LightService.Context {
    public Models.Order Order { get; set; }
    public int TaxPercentage { get; set; }
  }
}

// ORGANIZER
namespace LightServiceDemo.Organizers {
  public class CalculatesOrderTax : LightService.Organizer<OrderContext> {

    public static OrderContext ForOrder(Models.Order order) {
      // create a new context with the order
      // and call the relevant actions
      return With(ContextWithOrder(order)).Reduce(Actions());
    }

    private static OrderContext ContextWithOrder(Models.Order order) {
      return new OrderContext() { Order = order };
    }

    private static LightService.IAction<OrderContext>[] Actions() {
      return new LightService.IAction<OrderContext>[] {
        new Actions.CalculateTax(),
        new Actions.AddTaxToSubTotal()
      };
    }

  }
}

// ACTIONS
namespace LightServiceDemo.Actions {
  public class CalculateTax : LightService.IAction<OrderContext> {

    public OrderContext Executed(OrderContext context) {
      context.TaxPercentage = 7;
      return context;
    }

  }
}

namespace LightServiceDemo.Actions {
  public class AddTaxToSubTotal : LightService.IAction<OrderContext> {

    public OrderContext Executed(OrderContext context) {
      Order order = context.Order;
      order.SubTotal += TaxAmount(order.SubTotal, context.TaxPercentage);

      context.Order = order;
      return context;
    }

    private decimal TaxAmount(decimal subTotal, int taxPercentage) {
      return subTotal * (taxPercentage / (decimal) 100.0);
    }
  }
}



```

[Simple and Elegant Rails Code with Functional Style]: http://www.adomokos.com/2013/06/simple-and-elegant-rails-code-with.html
[Light Service]: https://github.com/adomokos/light-service
