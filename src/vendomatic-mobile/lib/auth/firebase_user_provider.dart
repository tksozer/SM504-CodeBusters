import 'package:firebase_auth/firebase_auth.dart';
import 'package:rxdart/rxdart.dart';

class VendomaticFirebaseUser {
  VendomaticFirebaseUser(this.user);
  User? user;
  bool get loggedIn => user != null;
}

VendomaticFirebaseUser? currentUser;
bool get loggedIn => currentUser?.loggedIn ?? false;
Stream<VendomaticFirebaseUser> vendomaticFirebaseUserStream() =>
    FirebaseAuth.instance
        .authStateChanges()
        .debounce((user) => user == null && !loggedIn
            ? TimerStream(true, const Duration(seconds: 1))
            : Stream.value(user))
        .map<VendomaticFirebaseUser>(
      (user) {
        currentUser = VendomaticFirebaseUser(user);
        return currentUser!;
      },
    );
