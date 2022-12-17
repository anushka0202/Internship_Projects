import React from "react";
import { Link } from "react-router-dom";
import user from "../images/user.jpg";

const PersonCard = (props) => {
  const { id, firstName, lastName, age } = props.person;
  return (
    <div className="item">
      <img className="ui avatar image" src={user} alt="user" />
      <div className="content">
        <Link
          to={{ pathname: `/person/${id}`, state: { person: props.person } }} // we create an object with the properties pathname and state (to pass name and age)
        >
          <div className="header">{firstName} {lastName}</div>
          <div className="description">{age}</div>

        </Link>
      </div>
      <i
        className="trash alternate outline icon"
        style={{ color: "Crimson", marginTop: "7px", marginLeft: "10px" }}
        onClick={() => props.clickHander(id)} //arrow fn takes a prop of clickHandler and with the prop it passes the id
      ></i>
      <Link to={{ pathname: `/edit`, state: { person: props.person } }}>
        <i
          className="edit alternate outline icon"
          style={{ color: "DodgerBlue", marginTop: "7px" }}
        ></i>
      </Link>
    </div>
  );
};

export default PersonCard;
