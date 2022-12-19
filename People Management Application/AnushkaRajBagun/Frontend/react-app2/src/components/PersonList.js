import React from "react";
import { Link } from "react-router-dom";
import PersonCard from "./PersonCard";

const PersonList = (props) => {
  console.log(props);

  const deleteConactHandler = (id) => {
    props.getPersonId(id); //we have passed getPersonId as a property from the App component
  };

  //function to render the list (takes the props of people and maps them i.e., map the person )
  const renderPersonList = props.people.map((person) => {
    return (
      <PersonCard
        person={person}
        clickHander={deleteConactHandler}
        key={person.id}
      />
    );
  });
  return (
    <div className="main">
      <h2>
        People List
        <Link to="/add">
          <button className="ui orange button right">
            Add Person
          </button>
        </Link>
      </h2>
      <div className="ui celled list">{renderPersonList}</div>
    </div>
  );
};

export default PersonList;
