// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'cities_record.dart';

// **************************************************************************
// BuiltValueGenerator
// **************************************************************************

Serializer<CitiesRecord> _$citiesRecordSerializer =
    new _$CitiesRecordSerializer();

class _$CitiesRecordSerializer implements StructuredSerializer<CitiesRecord> {
  @override
  final Iterable<Type> types = const [CitiesRecord, _$CitiesRecord];
  @override
  final String wireName = 'CitiesRecord';

  @override
  Iterable<Object?> serialize(Serializers serializers, CitiesRecord object,
      {FullType specifiedType = FullType.unspecified}) {
    final result = <Object?>[];
    Object? value;
    value = object.cityPhoto;
    if (value != null) {
      result
        ..add('cityPhoto')
        ..add(serializers.serialize(value,
            specifiedType: const FullType(String)));
    }
    value = object.cityName;
    if (value != null) {
      result
        ..add('cityName')
        ..add(serializers.serialize(value,
            specifiedType: const FullType(String)));
    }
    value = object.cityDescription;
    if (value != null) {
      result
        ..add('cityDescription')
        ..add(serializers.serialize(value,
            specifiedType: const FullType(String)));
    }
    value = object.founded;
    if (value != null) {
      result
        ..add('founded')
        ..add(serializers.serialize(value, specifiedType: const FullType(int)));
    }
    value = object.cityPopulation;
    if (value != null) {
      result
        ..add('cityPopulation')
        ..add(serializers.serialize(value, specifiedType: const FullType(int)));
    }
    value = object.index;
    if (value != null) {
      result
        ..add('index')
        ..add(serializers.serialize(value, specifiedType: const FullType(int)));
    }
    value = object.userLiked;
    if (value != null) {
      result
        ..add('userLiked')
        ..add(serializers.serialize(value,
            specifiedType: const FullType(BuiltList, const [
              const FullType(
                  DocumentReference, const [const FullType.nullable(Object)])
            ])));
    }
    value = object.userDisliked;
    if (value != null) {
      result
        ..add('userDisliked')
        ..add(serializers.serialize(value,
            specifiedType: const FullType(BuiltList, const [
              const FullType(
                  DocumentReference, const [const FullType.nullable(Object)])
            ])));
    }
    value = object.ffRef;
    if (value != null) {
      result
        ..add('Document__Reference__Field')
        ..add(serializers.serialize(value,
            specifiedType: const FullType(
                DocumentReference, const [const FullType.nullable(Object)])));
    }
    return result;
  }

  @override
  CitiesRecord deserialize(
      Serializers serializers, Iterable<Object?> serialized,
      {FullType specifiedType = FullType.unspecified}) {
    final result = new CitiesRecordBuilder();

    final iterator = serialized.iterator;
    while (iterator.moveNext()) {
      final key = iterator.current! as String;
      iterator.moveNext();
      final Object? value = iterator.current;
      switch (key) {
        case 'cityPhoto':
          result.cityPhoto = serializers.deserialize(value,
              specifiedType: const FullType(String)) as String?;
          break;
        case 'cityName':
          result.cityName = serializers.deserialize(value,
              specifiedType: const FullType(String)) as String?;
          break;
        case 'cityDescription':
          result.cityDescription = serializers.deserialize(value,
              specifiedType: const FullType(String)) as String?;
          break;
        case 'founded':
          result.founded = serializers.deserialize(value,
              specifiedType: const FullType(int)) as int?;
          break;
        case 'cityPopulation':
          result.cityPopulation = serializers.deserialize(value,
              specifiedType: const FullType(int)) as int?;
          break;
        case 'index':
          result.index = serializers.deserialize(value,
              specifiedType: const FullType(int)) as int?;
          break;
        case 'userLiked':
          result.userLiked.replace(serializers.deserialize(value,
              specifiedType: const FullType(BuiltList, const [
                const FullType(
                    DocumentReference, const [const FullType.nullable(Object)])
              ]))! as BuiltList<Object?>);
          break;
        case 'userDisliked':
          result.userDisliked.replace(serializers.deserialize(value,
              specifiedType: const FullType(BuiltList, const [
                const FullType(
                    DocumentReference, const [const FullType.nullable(Object)])
              ]))! as BuiltList<Object?>);
          break;
        case 'Document__Reference__Field':
          result.ffRef = serializers.deserialize(value,
              specifiedType: const FullType(DocumentReference, const [
                const FullType.nullable(Object)
              ])) as DocumentReference<Object?>?;
          break;
      }
    }

    return result.build();
  }
}

class _$CitiesRecord extends CitiesRecord {
  @override
  final String? cityPhoto;
  @override
  final String? cityName;
  @override
  final String? cityDescription;
  @override
  final int? founded;
  @override
  final int? cityPopulation;
  @override
  final int? index;
  @override
  final BuiltList<DocumentReference<Object?>>? userLiked;
  @override
  final BuiltList<DocumentReference<Object?>>? userDisliked;
  @override
  final DocumentReference<Object?>? ffRef;

  factory _$CitiesRecord([void Function(CitiesRecordBuilder)? updates]) =>
      (new CitiesRecordBuilder()..update(updates))._build();

  _$CitiesRecord._(
      {this.cityPhoto,
      this.cityName,
      this.cityDescription,
      this.founded,
      this.cityPopulation,
      this.index,
      this.userLiked,
      this.userDisliked,
      this.ffRef})
      : super._();

  @override
  CitiesRecord rebuild(void Function(CitiesRecordBuilder) updates) =>
      (toBuilder()..update(updates)).build();

  @override
  CitiesRecordBuilder toBuilder() => new CitiesRecordBuilder()..replace(this);

  @override
  bool operator ==(Object other) {
    if (identical(other, this)) return true;
    return other is CitiesRecord &&
        cityPhoto == other.cityPhoto &&
        cityName == other.cityName &&
        cityDescription == other.cityDescription &&
        founded == other.founded &&
        cityPopulation == other.cityPopulation &&
        index == other.index &&
        userLiked == other.userLiked &&
        userDisliked == other.userDisliked &&
        ffRef == other.ffRef;
  }

  @override
  int get hashCode {
    var _$hash = 0;
    _$hash = $jc(_$hash, cityPhoto.hashCode);
    _$hash = $jc(_$hash, cityName.hashCode);
    _$hash = $jc(_$hash, cityDescription.hashCode);
    _$hash = $jc(_$hash, founded.hashCode);
    _$hash = $jc(_$hash, cityPopulation.hashCode);
    _$hash = $jc(_$hash, index.hashCode);
    _$hash = $jc(_$hash, userLiked.hashCode);
    _$hash = $jc(_$hash, userDisliked.hashCode);
    _$hash = $jc(_$hash, ffRef.hashCode);
    _$hash = $jf(_$hash);
    return _$hash;
  }

  @override
  String toString() {
    return (newBuiltValueToStringHelper(r'CitiesRecord')
          ..add('cityPhoto', cityPhoto)
          ..add('cityName', cityName)
          ..add('cityDescription', cityDescription)
          ..add('founded', founded)
          ..add('cityPopulation', cityPopulation)
          ..add('index', index)
          ..add('userLiked', userLiked)
          ..add('userDisliked', userDisliked)
          ..add('ffRef', ffRef))
        .toString();
  }
}

class CitiesRecordBuilder
    implements Builder<CitiesRecord, CitiesRecordBuilder> {
  _$CitiesRecord? _$v;

  String? _cityPhoto;
  String? get cityPhoto => _$this._cityPhoto;
  set cityPhoto(String? cityPhoto) => _$this._cityPhoto = cityPhoto;

  String? _cityName;
  String? get cityName => _$this._cityName;
  set cityName(String? cityName) => _$this._cityName = cityName;

  String? _cityDescription;
  String? get cityDescription => _$this._cityDescription;
  set cityDescription(String? cityDescription) =>
      _$this._cityDescription = cityDescription;

  int? _founded;
  int? get founded => _$this._founded;
  set founded(int? founded) => _$this._founded = founded;

  int? _cityPopulation;
  int? get cityPopulation => _$this._cityPopulation;
  set cityPopulation(int? cityPopulation) =>
      _$this._cityPopulation = cityPopulation;

  int? _index;
  int? get index => _$this._index;
  set index(int? index) => _$this._index = index;

  ListBuilder<DocumentReference<Object?>>? _userLiked;
  ListBuilder<DocumentReference<Object?>> get userLiked =>
      _$this._userLiked ??= new ListBuilder<DocumentReference<Object?>>();
  set userLiked(ListBuilder<DocumentReference<Object?>>? userLiked) =>
      _$this._userLiked = userLiked;

  ListBuilder<DocumentReference<Object?>>? _userDisliked;
  ListBuilder<DocumentReference<Object?>> get userDisliked =>
      _$this._userDisliked ??= new ListBuilder<DocumentReference<Object?>>();
  set userDisliked(ListBuilder<DocumentReference<Object?>>? userDisliked) =>
      _$this._userDisliked = userDisliked;

  DocumentReference<Object?>? _ffRef;
  DocumentReference<Object?>? get ffRef => _$this._ffRef;
  set ffRef(DocumentReference<Object?>? ffRef) => _$this._ffRef = ffRef;

  CitiesRecordBuilder() {
    CitiesRecord._initializeBuilder(this);
  }

  CitiesRecordBuilder get _$this {
    final $v = _$v;
    if ($v != null) {
      _cityPhoto = $v.cityPhoto;
      _cityName = $v.cityName;
      _cityDescription = $v.cityDescription;
      _founded = $v.founded;
      _cityPopulation = $v.cityPopulation;
      _index = $v.index;
      _userLiked = $v.userLiked?.toBuilder();
      _userDisliked = $v.userDisliked?.toBuilder();
      _ffRef = $v.ffRef;
      _$v = null;
    }
    return this;
  }

  @override
  void replace(CitiesRecord other) {
    ArgumentError.checkNotNull(other, 'other');
    _$v = other as _$CitiesRecord;
  }

  @override
  void update(void Function(CitiesRecordBuilder)? updates) {
    if (updates != null) updates(this);
  }

  @override
  CitiesRecord build() => _build();

  _$CitiesRecord _build() {
    _$CitiesRecord _$result;
    try {
      _$result = _$v ??
          new _$CitiesRecord._(
              cityPhoto: cityPhoto,
              cityName: cityName,
              cityDescription: cityDescription,
              founded: founded,
              cityPopulation: cityPopulation,
              index: index,
              userLiked: _userLiked?.build(),
              userDisliked: _userDisliked?.build(),
              ffRef: ffRef);
    } catch (_) {
      late String _$failedField;
      try {
        _$failedField = 'userLiked';
        _userLiked?.build();
        _$failedField = 'userDisliked';
        _userDisliked?.build();
      } catch (e) {
        throw new BuiltValueNestedFieldError(
            r'CitiesRecord', _$failedField, e.toString());
      }
      rethrow;
    }
    replace(_$result);
    return _$result;
  }
}

// ignore_for_file: deprecated_member_use_from_same_package,type=lint
