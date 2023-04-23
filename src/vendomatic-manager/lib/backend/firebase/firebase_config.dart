import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/foundation.dart';

Future initFirebase() async {
  if (kIsWeb) {
    await Firebase.initializeApp(
        options: FirebaseOptions(
            apiKey: "AIzaSyB6ZniGA1QtqL496NApylzmm1oRN72PG_I",
            authDomain: "vendomatic-manager.firebaseapp.com",
            projectId: "vendomatic-manager",
            storageBucket: "vendomatic-manager.appspot.com",
            messagingSenderId: "32955973920",
            appId: "1:32955973920:web:10fa5fe5dcd4c7ce821745",
            measurementId: "G-E9RT9K026N"));
  } else {
    await Firebase.initializeApp();
  }
}
