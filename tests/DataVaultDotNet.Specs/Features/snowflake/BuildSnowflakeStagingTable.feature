Feature: Build Snowflake staging table

  As a pipeline engineer
  I want the pipeline to create or validate the Snowflake staging table
  So that the data load can proceed with a known structure

  Background:
    Given a source model named "CustomerModel" with fields:
      | Name         | Type    |
      | CustomerId   | string  |
      | FullName     | string  |
      | Email        | string  |

  Scenario: Generate staging table for Snowflake
    Given the target database is "Snowflake"
    And the staging table is named "stg_Customer"
    When the build step is executed
    Then the staging table "stg_Customer" should exist in the database
    And it should have columns:
      | ColumnName   | DataType   |
      | CustomerId   | STRING     |
      | FullName     | STRING     |
      | Email        | STRING     |
    And the table should match the source model schema
