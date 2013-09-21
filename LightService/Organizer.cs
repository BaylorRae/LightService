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

        public static Organizer<T> With(T context = null) {
            if( context == null ) {
                context = (T) new Context();
            }

            return new Organizer<T>(context);
        }

        public T Reduce(IAction<T>[] actions) {
            foreach( IAction<T> action in actions ) {
                this.context = action.Executed(this.context);
            }

            return this.context;
        }
    }
}
