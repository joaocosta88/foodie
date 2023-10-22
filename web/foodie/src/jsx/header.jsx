import "../css/header.css";

const Header = () => {
    return (
      <nav className="header-container">
      <div className="logo">
        Foodie!
      </div>
      <div className="auth-container">
        <div>
            Login
        </div>
        <div>
            Signin
        </div>
      </div>
    </nav>
    );
  };
  
  export default Header;