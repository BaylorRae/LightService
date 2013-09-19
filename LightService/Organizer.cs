using System;

namespace LightService {
    public class Organizer {
        public Context context { get; private set; }

        public Organizer() {
            this.context = new Context();
        }

        public Organizer(Context context) {
            this.context = context;
        }

        public static Organizer With(Context context = null) {
            if( context == null ) {
                context = new Context();
            }

            return new Organizer(context);
        }

        public void Reduce(IAction[] actions) {
            foreach( IAction action in actions ) {
                action.Executed();
            }
        }
    }
}
