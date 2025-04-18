Feature: Copy source data into a Snowflake staging table

  As a pipeline engineer
  I want to load data from a source query into a Snowflake staging table
  So that I can begin the raw vault processing step

  Background:
    Given a source named "CustomerImport"
    And the source SQL is:
      """
      SELECT * FROM legacy_customer
      """

  Scenario: Load data to Snowflake staging table using COPY INTO
    Given the target database is "Snowflake"
    And the staging table is named "stg_Customer"
    When the pipeline is executed
    Then the staging table "stg_Customer" should contain all rows from the source query
    And the load should be done using a COPY INTO operation
    And the row count in "stg_Customer" should equal the row count of the source query
