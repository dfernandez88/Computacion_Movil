package com.example.darilaptop.concentrese_dari;

import android.app.Activity;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Random;


public class gameeasy extends Activity {
    public static int[] colors = {Color.BLACK,Color.YELLOW, Color.BLUE, Color.RED, Color.CYAN, Color.GREEN};
    public static ArrayList<Integer> tipe_colors = new ArrayList<Integer>();
    public static int state = 0;
    public static int actplayer = 0;
    public static int tagger = 0;
    public static ArrayList<Boolean> ok = new ArrayList<Boolean>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_gameeasy);
        createboard();
    }

    private void createboard() {
        actplayer = 0;
        tipe_colors.clear();
        ArrayList<Integer> tipe = new ArrayList<Integer>(Arrays.asList(0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5));
        ok.clear();
        for (int i = 0; i < 12; i++) {
            ok.add(false);
        }
        Random rnd = new Random();
        int c = 1;
        while (tipe.size() > 0) {
            int tmp = 0;
            if (tipe.size() != 1) {
                tmp = (rnd.nextInt((tipe.size() - 1) + 1));
            }
            tipe_colors.add(colors[tipe.get(tmp)]);
            tipe.remove(tmp);
            Button b = (Button) findViewById(getResources().getIdentifier("boton" + Integer.toString(c), "id", getPackageName()));
            b.setBackgroundColor(Color.DKGRAY);
            c++;
        }
    }


    public void play(View view) {
        final Button button = (Button) view;
        int tag = Integer.parseInt(button.getTag().toString());
        if (!ok.get(tag - 1)) if (state == 0) {
            state = 1;
            button.setBackgroundColor(tipe_colors.get(tag - 1));
            tagger = tag;
        } else {
            state = 0;
            Log.d("tagger", Integer.toString(tagger));
            Log.d("tipe_color(tagger-1)", Integer.toString(tipe_colors.get(tagger - 1)));
            Log.d("tag", Integer.toString(tag));
            Log.d("tipe_color(tag-1)", Integer.toString(tipe_colors.get(tag - 1)));

            if ((int) tipe_colors.get(tagger - 1) == (int) tipe_colors.get(tag - 1)) {
                final Button b = (Button) findViewById(getResources().getIdentifier("boton" + Integer.toString(tagger), "id", getPackageName()));
                button.setBackgroundColor(tipe_colors.get(tag - 1));
                new Handler().postDelayed(new Runnable() {
                    @Override
                    public void run() {
                        b.setBackgroundColor(Color.WHITE);
                        button.setBackgroundColor(Color.WHITE);

                    }
                }, 1000);

                ok.set(tagger - 1, true);
                ok.set(tag - 1, true);
                Toast.makeText(getApplicationContext(), getResources().getString(R.string.acerto),
                        Toast.LENGTH_SHORT).show();

                for (int i = 0; i < ok.size(); i++) {
                    if (ok.get(i) == false) {
                        return;
                    }
                }
            } else {
                button.setBackgroundColor(tipe_colors.get(tag - 1));
                final Button b = (Button) findViewById(getResources().getIdentifier("boton" + Integer.toString(tagger), "id", getPackageName()));
                new Handler().postDelayed(new Runnable() {
                    @Override
                    public void run() {
                        b.setBackgroundColor(Color.GRAY);
                        button.setBackgroundColor(Color.GRAY);
                    }
                }, 1000);
                actplayer++;
            }
        }
    }
}


