using System;

namespace LightService {
    public class Organizer<T> where T : Context {
        public T context { get; private set; }

        public Organizer() {
        }

        public Organizer(Context context) {
            this.context = (T) context;
        }

        public Organizer(T context) {
            this.context = context;
        }

        public static Organizer<T> With(Context context = null) {
            if( context == null ) {
                context = new Context();
            }

            return new Organizer<T>(context);
        }

        public Context Reduce(IAction<T>[] actions) {
            foreach( IAction<T> action in actions ) {
                this.context = action.Executed(this.context);
            }

            return this.context;
        }
    }
}
