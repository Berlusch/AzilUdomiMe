import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import useAuth from '../hooks/useAuth';

export default function Login() {
  const { login } = useAuth();

  function handleSubmit(e) {
    e.preventDefault();

    const podatci = new FormData(e.target);
    login({
      email: podatci.get('email'),
      password: podatci.get('lozinka'),
    });
  }

  return (
    <Container className='mt-4'>   
     <div className="loginPodatci">
      <p>email:  bernarda.lusch@gmail.com</p>
      <p>lozinka:  kadulja</p> 
        </div>   
      <Form onSubmit={handleSubmit}>
        <Form.Group className='mb-3' controlId='email'>
          <Form.Label>Email</Form.Label>
          <Form.Control
            type='text'
            name='email'
            placeholder='bernarda.lusch@gmail.com'
            maxLength={255}
            required
          />
        </Form.Group>
        <Form.Group className='mb-3' controlId='lozinka'>
          <Form.Label>Lozinka</Form.Label>
          <Form.Control type='password' name='lozinka' required />
        </Form.Group>
        <Button className="gumb" type="submit">
        Autoriziraj
      </Button>
      </Form>
    </Container>
  );
}