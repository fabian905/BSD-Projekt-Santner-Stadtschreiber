package com.example.fabian.schadensmeldungen;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;


public class LoginActivity extends AppCompatActivity {

    Button anmelden;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        anmelden = (Button) findViewById(R.id.bttnAnmelden);
    }

    public void onClickAnmelden(View v) {
        Intent showAll = new Intent(LoginActivity.this, MainActivity.class);
        startActivity(showAll);
    }
}
