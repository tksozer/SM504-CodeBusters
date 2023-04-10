import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/foundation.dart';

Future initFirebase() async {
  if (kIsWeb) {
    await Firebase.initializeApp(
        options: FirebaseOptions(
            apiKey: "AIzaSyDw058HISehsY8dldCNq1z4h3aW5E5Csbg",
            authDomain: "vendomatic-2d85e.firebaseapp.com",
            projectId: "vendomatic-2d85e",
            storageBucket: "vendomatic-2d85e.appspot.com",
            messagingSenderId: "515543385290",
            appId: "1:515543385290:web:49ee31e36b80f3022ce033",
            measurementId: "G-0G96DFHCEP"));
  } else {
    await Firebase.initializeApp();
  }
}
