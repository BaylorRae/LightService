using System;

namespace LightService {
    public class Organizer {
        public Context context = null;

        public Organizer() {
            this.context = new Context();
        }

        public static Organizer With() {
            return new Organizer();
        }

    }
}
