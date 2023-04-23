import 'dart:async';

import 'index.dart';
import 'serializers.dart';
import 'package:built_value/built_value.dart';

part 'cities_record.g.dart';

abstract class CitiesRecord
    implements Built<CitiesRecord, CitiesRecordBuilder> {
  static Serializer<CitiesRecord> get serializer => _$citiesRecordSerializer;

  String? get cityPhoto;

  String? get cityName;

  String? get cityDescription;

  int? get founded;

  int? get cityPopulation;

  int? get index;

  BuiltList<DocumentReference>? get userLiked;

  BuiltList<DocumentReference>? get userDisliked;

  @BuiltValueField(wireName: kDocumentReferenceField)
  DocumentReference? get ffRef;
  DocumentReference get reference => ffRef!;

  static void _initializeBuilder(CitiesRecordBuilder builder) => builder
    ..cityPhoto = ''
    ..cityName = ''
    ..cityDescription = ''
    ..founded = 0
    ..cityPopulation = 0
    ..index = 0
    ..userLiked = ListBuilder()
    ..userDisliked = ListBuilder();

  static CollectionReference get collection =>
      FirebaseFirestore.instance.collection('cities');

  static Stream<CitiesRecord> getDocument(DocumentReference ref) => ref
      .snapshots()
      .map((s) => serializers.deserializeWith(serializer, serializedData(s))!);

  static Future<CitiesRecord> getDocumentOnce(DocumentReference ref) => ref
      .get()
      .then((s) => serializers.deserializeWith(serializer, serializedData(s))!);

  CitiesRecord._();
  factory CitiesRecord([void Function(CitiesRecordBuilder) updates]) =
      _$CitiesRecord;

  static CitiesRecord getDocumentFromData(
          Map<String, dynamic> data, DocumentReference reference) =>
      serializers.deserializeWith(serializer,
          {...mapFromFirestore(data), kDocumentReferenceField: reference})!;
}

Map<String, dynamic> createCitiesRecordData({
  String? cityPhoto,
  String? cityName,
  String? cityDescription,
  int? founded,
  int? cityPopulation,
  int? index,
}) {
  final firestoreData = serializers.toFirestore(
    CitiesRecord.serializer,
    CitiesRecord(
      (c) => c
        ..cityPhoto = cityPhoto
        ..cityName = cityName
        ..cityDescription = cityDescription
        ..founded = founded
        ..cityPopulation = cityPopulation
        ..index = index
        ..userLiked = null
        ..userDisliked = null,
    ),
  );

  return firestoreData;
}
