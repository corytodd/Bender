﻿namespace Bender.Core.Tests
{
    static class TestData
    {
        public static readonly string SimpleTest = @"---
format: bender.v1
# Unique name for this layout
name: simple_layout
# Friendly description of what your binary file does
description: >
    A simple sample binary layout descriptor
    that can span multiple lines
    
# List of known extensions for this type
extensions:
    - test
    - simple

base_element: &base_element
    name: Undefined
    elide: no
    units: 4            # Bytes wide (1, 2, 4 etc.)
    signed: no          # Is integer signed
    format: ascii       # binary, decimal, octal, hex, ascii, unicode, bigint, float
    little_endian: yes  # byte storage order
    matrix:             # Matrix definition, optional
    is_deferred: false  # Is this pointing to more data

structures:
    # A named, ordered list of elements
    - name: Range
      elements:
        - <<: *base_element
          name: Pair Count
          units: 4
          format: decimal
        - <<: *base_element
          name: Start
          units: 4
          format: decimal
        - <<: *base_element
          name: End
          units: 4
          format: decimal

enumerations:
    - name: enum1
      values:
        0: Zero
        1: One

elements:
    # A name descriptor for a sequence of bytes
    - <<: *base_element
      name: Magic Number

    - <<: *base_element
      format: decimal
      units: 1
      name: Version
      
    - <<: *base_element
      signed: yes
      format: decimal
      units: 1
      name: Counter

    - <<: *base_element
      format: binary
      units: 1
      name: Flags

    - <<: *base_element
      little_endian: no
      format: hex
      units: 2
      name: CRC16

    - <<: *base_element
      elide: yes
      format: hex
      units: 16
      name: Payload
      
    - <<: *base_element     
      format: decimal
      units: 16
      name: Transform A    
      matrix:
        columns: 4
        units: 1
      
    - <<: *base_element     
      format: decimal
      units: 16
      name: Transform B   
      matrix:
        columns: 2
        units: 2     
    
    - <<: *base_element
      enumeration: enum1
      name: Some Enumeration
      units: 1  
    
layout:
    # References any structure or element defined above
    - Magic Number
    - Version
    - Counter
    - Flags
    - CRC16
    - Payload
    - Transform A  
    - Transform B 
    - Range
    - Some Enumeration";

        public static readonly string SignedTest = @"---
format: bender.v1
name: test_signed
description: >
    A test targeting signed versions of all
    integer types

extensions:
    - signed

base_element: &base
    little_endian: yes
    elide: no
    format: decimal
    units: 1
    name: Undefined
    signed: yes

elements:
    - <<: *base
      name: Signed Byte

    - <<: *base
      units: 2
      name: Signed Short

    - <<: *base
      units: 4
      name: Signed Int

    - <<: *base
      units: 8
      name: Signed Long

    - <<: *base
      units: 2
      little_endian: no
      name: Signed Short BE

    - <<: *base
      units: 4
      little_endian: no      
      name: Signed Int BE

    - <<: *base
      units: 8
      little_endian: no      
      name: Signed Long BE";

        public static readonly string UnsignedTest  = @"---
format: bender.v1
name: test_unsigned
description: >
    A test targeting unsigned versions of all
    integer types

extensions:
    - unsigned

base_element: &base
    little_endian: yes
    elide: no
    format: decimal
    units: 1
    name: Undefined
    signed: no

elements:
    - <<: *base
      name: Unsigned Byte

    - <<: *base
      units: 2
      name: Unsigned Short

    - <<: *base
      units: 4
      name: Unsigned Int

    - <<: *base
      units: 8
      name: Unsigned Long

    - <<: *base
      units: 2
      little_endian: no         
      name: Unsigned Short

    - <<: *base
      units: 4
      little_endian: no         
      name: Unsigned Int

    - <<: *base
      units: 8
      little_endian: no         
      name: Unsigned Long";

        public static readonly string StringTest = @"---
format: bender.v1
name: test_strings
description: >
    A test targeting string types

extensions:
    - test
    - strings

base_element: &base
    little_endian: yes
    elide: no
    format: ascii
    units: 1
    name: Undefined
    signed: yes

elements:
    - <<: *base
      name: ascii     
      units: 4

    - <<: *base
      format: unicode
      units: 8
      
    - <<: *base
      name: ascii     
      units: 4
      little_endian: no      

    - <<: *base
      format: unicode
      units: 8      
      little_endian: no";

        public static readonly string TestInvalidYAML = @"---
format: bender.v1
name: test_invalid_yaml
description: >
    A test targeting marlformed YAML

extensions:
    - invalid

# Base merge key specifier '*' should be '&'
base_element: *base_element
    little_endian: yes
    elide: no
    format: decimal
    units: 1
    name: Undefined
    signed: no

elements:
    - <<: *base_element
    # bad ident
    name: Unsigned Byte";

        public static readonly string TestMatrices = @"---
format: bender.v1
name: test_missing_matrix
description: >
    A test that has a missing matrix definition

extensions:
    - matrices

base_element: &base_element
    little_endian: yes
    elide: no
    format: octal
    units: 1
    name: Undefined
    signed: no

elements:
    - <<: *base_element
      # Should not halt the program
      name: No matrix defined
      
    - <<: *base_element
      name: No matrix tag";

        public static readonly string TestDeferred = @"---
format: bender.v1
name: test_deferred
description: >
    A test targeting deferred data reading. Deferred data
    is where a size and offset record are placed in a static
    space which refer to a dynamic block of data someplace else
    in the file.

extensions:
    - deferred

base_element: &base_element
    little_endian: yes
    elide: no
    format: decimal
    units: 1
    name: Undefined
    signed: no

elements:
    - <<: *base_element
      name: Magic Number
      units: 8
      format: ascii
      
    - <<: *base_element
      name: Blob Test
      is_deferred: true
      format: hex
      # There is a 2x1 matrix at this location
      # Size and offset are implied 
      matrix:
        columns: 2
        units: 1
     
    - <<: *base_element
      name: Meaning of Life
      units: 4
      format: decimal";
    }
}
